
using AutoMapper;
using Infrastructure.Modules.Contents.Entities;
using Infrastructure.Modules.Contents.Requests;
using Infrastructure.Modules.ContentSessions.Requests;
using Infrastructure.Modules.Items.Entities;
using Infrastructure.Modules.Items.Requests;
using Infrastructure.Modules.Sessions.Entities;
using Infrastructure.Modules.Sessions.Requests;
using Infrastructure.Modules.Users.Entities;
using Infrastructure.Modules.Users.Requests;

namespace Infrastructure.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UpdateItemRequest, Item>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember is not null || !string.IsNullOrWhiteSpace((string?)srcMember)));
        CreateMap<UpdateItemsRequest, Item>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember is not null || !string.IsNullOrWhiteSpace((string?)srcMember)));
        CreateMap<CreateItemRequest, Item>();
        CreateMap<CreateContentRequest, Content>();
        CreateMap<CreateContentSessionRequest, ContentSession>();
        CreateMap<UpdateContentSessionRequest, ContentSession>();
        CreateMap<UpdateContentRequest, Content>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember is not null || !string.IsNullOrWhiteSpace((string?)srcMember))); ;
        CreateMap<CreateSessionItemRequest, Item>();
        CreateMap<CreateSessionRequest, Session>();
        CreateMap<UpdateSessionRequest, Session>();
        CreateMap<UpdateUserRequest, User>();
        CreateMap<CreateUserRequest, User>()
            .ForMember(dest => dest.Avatar, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom((src, dest) => dest.PasswordHash = BCrypt.Net.BCrypt.HashPassword(src.Password!)));

    }
}
