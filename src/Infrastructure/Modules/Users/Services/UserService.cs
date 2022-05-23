using AutoMapper;
using Core.Common.Interfaces;
using Infrastructure.Definitions;
using Infrastructure.Modules.Users.Entities;
using Infrastructure.Modules.Users.Requests;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Modules.Users.Services;

public interface IUserService : IScopedService
{
    Task<User> CreateAsync(CreateUserRequest request);
    Task DeleteAsync(User user);
    Task<string?> CheckCode(CheckCodeRequest request);
    Task<User> UpdateAsync(User user, UpdateUserRequest request);
    Task<string> UpdateCodeAsync(User user);
    Task<PaginationResponse<User>> GetAllAsync(PaginationRequest request);
    Task<(User? User, string? ErrorMessage)> GetByIdAsync(Guid id);
    Task<(User? User, string? ErrorMessage)> GetByEmaillAsync(string email);
    Task<(string? Token, string? ErrorMessage)> Login(LoginRequest request);
    Task<(User? User, string? ErrorMessage)> ChangePasswordAsync(Guid userId, ChangePasswordRequest request);
    Task<string?> ResetPasswordAsync(ResetPasswordRequest request);
    Task<User?> GetProfile(Guid userId);
    string GenerateCallbackLink(string code);


    string GenerateJwtToken(User user);
}

public class UserService : IUserService
{
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public UserService(IRepositoryWrapper repositoryWrapper, IMapper mapper, IConfiguration configuration)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<User> CreateAsync(CreateUserRequest request)
    {
        User? user = _mapper.Map<User>(request);
        await _repositoryWrapper.Users.AddAsync(user);
        return user;
    }

    public async Task DeleteAsync(User user)
    {
        await _repositoryWrapper.Users.DeleteAsync(user);
    }

    public async Task<string?> CheckCode(CheckCodeRequest request)
    {
        (User? user, string? errorMessage) = await GetByEmaillAsync(request.EmailAddress!);
        if (errorMessage is not null) return (errorMessage);
        if (user!.Code != request.Code) return (Messages.Users.WrongCode);
        if (user!.CodeExpires < DateTime.UtcNow) return (Messages.Users.CodeIsExpires);
        return null;
    }

    public async Task<PaginationResponse<User>> GetAllAsync(PaginationRequest request)
    {
        //IQueryable<User> users = _repositoryWrapper.Users.Find();
        //SearchUtility<User>.ApplySearch(ref users, request.SearchContent!, "FullName,EmailAddress");
        IQueryable<User> users = _repositoryWrapper.Users.Find(x => request.SearchContent == null
                                                                    || x.FullName!.ToLower().Contains(request.SearchContent.ToLower())
                                                                    || x.EmailAddress!.ToLower().Contains(request.SearchContent.ToLower()));
        users = SortUtility<User>.ApplySort(users, request.OrderByQuery);
        PaginationUtility<User> data = await PaginationUtility<User>.ToPagedListAsync(users, request.Current, request.PageSize);
        return PaginationResponse<User>.PaginationInfo(data, data.PageInfo);
    }

    public async Task<(User? User, string? ErrorMessage)> GetByEmaillAsync(string email)
    {
        User? user = await _repositoryWrapper.Users.Find(x => x.EmailAddress == email).FirstOrDefaultAsync();
        if (user is null) return (null, Messages.Users.EmailNotFound);
        return (user, null);
    }

    public async Task<(string? Token, string? ErrorMessage)> Login(LoginRequest request)
    {
        (User? user, string? errorMessage) = await GetByEmaillAsync(request.EmailAddress!);
        if (errorMessage is not null) return (null, errorMessage);
        bool verified = BCrypt.Net.BCrypt.Verify(request.Password, user!.PasswordHash);
        return verified ? (GenerateJwtToken(user), null) : (null, Messages.Users.WrongPassword);
    }

    public async Task<(User? User, string? ErrorMessage)> ChangePasswordAsync(Guid userId, ChangePasswordRequest request)
    {
        (User? user, string? errorMessage) = await GetByIdAsync(userId);
        if (errorMessage is not null) return (null, errorMessage);
        bool verified = BCrypt.Net.BCrypt.Verify(request.OldPassword, user!.PasswordHash);
        if (!verified) return (null, Messages.Users.WrongPassword);
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
        await _repositoryWrapper.Users.UpdateAsync(user);
        return (user, null);
    }

    public async Task<string?> ResetPasswordAsync(ResetPasswordRequest request)
    {
        (User? user, string? errorMessage) = await GetByEmaillAsync(request.EmailAddress!);
        if (errorMessage is not null) return (errorMessage);
        if (user!.Code != request.Code) return (Messages.Users.WrongCode);
        if (user!.CodeExpires < DateTime.UtcNow) return (Messages.Users.CodeIsExpires);
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        await _repositoryWrapper.Users.UpdateAsync(user);
        return null;
    }

    public async Task<User?> GetProfile(Guid userId)
    {
        return await _repositoryWrapper.Users.GetByIdAsync(userId);
    }

    public string GenerateCallbackLink(string code)
    {
        return QueryHelpers.AddQueryString(_configuration["MailSettings:CallbackLink"], "code", code);
    }


    public async Task<(User? User, string? ErrorMessage)> GetByIdAsync(Guid id)
    {
        User? user = await _repositoryWrapper.Users.Find(x => x.Id == id).FirstOrDefaultAsync();
        if (user is null) return (null, Messages.Users.IdNotFound);
        return (user, null);
    }

    public async Task<User> UpdateAsync(User user, UpdateUserRequest request)
    {
        _mapper.Map(request, user);
        await _repositoryWrapper.Users.UpdateAsync(user);
        return user;
    }

    public string GenerateJwtToken(User user)
    {
        JwtSecurityTokenHandler jwtTokenHandler = new JwtSecurityTokenHandler();
        byte[] secretKey = Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"]);
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
        {

            Audience = _configuration["JwtSettings:Issuer"],
            Issuer = _configuration["JwtSettings:Issuer"],
            Subject = new ClaimsIdentity(new[]
            {
                    new Claim(JwtClaimsName.Identification,user.Id.ToString()),
                    new Claim(JwtClaimsName.EmailAddress,user.EmailAddress!),
                    new Claim(JwtClaimsName.FullName, user.FullName!),
            }),
            Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JwtSettings:ExpiredTime"])),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
        };

        return jwtTokenHandler.WriteToken(jwtTokenHandler.CreateToken(tokenDescriptor));
    }

    public async Task<string> UpdateCodeAsync(User user)
    {
        Random random = new Random();
        string code = random.Next(0, 1000000).ToString("D6");
        user!.Code = code;
        user!.CodeExpires = DateTime.UtcNow.AddMinutes(5);
        await _repositoryWrapper.Users.UpdateAsync(user);
        return code;
    }
}
