using System.Net.Security;
using System.Security.Claims;
using Hangfire;
using Infrastructure.Definitions;
using Infrastructure.Modules.Users.Entities;
using Infrastructure.Modules.Users.Requests;
using Infrastructure.Modules.Users.Services;
using Infrastructure.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Users
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        public UsersController(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequest request)
        {
            PaginationResponse<User> users = await _userService.GetAllAsync(request);
            return Ok(users, Messages.Users.GetSuccessfully);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetById(Guid userId)
        {
            (User? user, string? errorMessage) = await _userService.GetByIdAsync(userId);
            return errorMessage is not null ? BadRequest(errorMessage) : Ok(user, Messages.Users.GetSuccessfully);
        }

        [HttpGet("Profile")]
        public async Task<IActionResult> GetProfile()
        {
            Guid userId = Guid.Parse(User.FindFirstValue(JwtClaimsName.Identification));
            User? user = await _userService.GetProfile(userId);
            return Ok(user, Messages.Users.GetSuccessfully);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(CreateUserRequest request)
        {
            User user = await _userService.CreateAsync(request);
            return Ok(user, Messages.Users.RegisterSuccessfully);
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            (string? token, string? errorMessage) = await _userService.Login(request);
            return errorMessage is not null ? BadRequest(errorMessage) : Ok(token, Messages.Users.LoginSuccessfully);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserRequest request)
        {
            Guid userId = Guid.Parse(User.FindFirstValue(JwtClaimsName.Identification));
            (User? user, string? errorMessage) = await _userService.GetByIdAsync(userId);
            if (errorMessage is not null) return BadRequest(errorMessage);
            await _userService.UpdateAsync(user!, request);
            return Ok(user, Messages.Users.UpdateSuccessfully);
        }

        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            Guid userId = Guid.Parse(User.FindFirstValue(JwtClaimsName.Identification));
            (User? user, string? errorMessage) = await _userService.ChangePasswordAsync(userId, request);
            return errorMessage is not null ? BadRequest(errorMessage) : Ok(user, Messages.Users.UpdateSuccessfully);
        }
        [AllowAnonymous]
        [HttpPost("CheckCode")]
        public async Task<IActionResult> CheckCode(CheckCodeRequest request)
        {
            string? errorMessage = await _userService.CheckCode(request);
            if (errorMessage is not null) return BadRequest(errorMessage);
            return Ok(null, Messages.Users.CheckCodeSuccessfully);
        }
        [AllowAnonymous]
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            (User? user, string? errorMessage) = await _userService.GetByEmaillAsync(email);
            if (errorMessage is not null) return BadRequest(errorMessage);
            string code = await _userService.UpdateCodeAsync(user!);
            string callbackUrl = _userService.GenerateCallbackLink(code);

            string html = $"<h1 >Hello <a href=\"{callbackUrl}\">Click me</a></h1> ";
            _emailService.Send(email, "Reset password", html);
            return Ok(null, Messages.Users.SendMailSuccessfully);
        }
        [AllowAnonymous]
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            string? errorMessage = await _userService.ResetPasswordAsync(request);
            if (errorMessage is not null) return BadRequest(errorMessage);
            return Ok(null, Messages.Users.ResetPasswordlSuccessfully);
        }

    }
}
