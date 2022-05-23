
using AutoMapper;
using Infrastructure.Modules.Users.Entities;
using Infrastructure.Modules.Users.Requests;

namespace Infrastructure.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        CreateMap<UpdateUserRequest, User>();
        CreateMap<CreateUserRequest, User>().
            ForMember(dest => dest.PasswordHash, opt => opt.MapFrom((src, dest) => dest.PasswordHash = BCrypt.Net.BCrypt.HashPassword(src.Password!)));

    }
}
