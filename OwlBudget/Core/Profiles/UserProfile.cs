using AutoMapper;
using Core.DAL.Entities;
using Core.Domain;

namespace Core.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserEntity, User>()
            .ForMember(_ => _.Role, configuration => configuration.MapFrom(_ => _.RoleId));
        CreateMap<User, UserEntity>()
            .ForMember(_ => _.RoleId, configuration => configuration.MapFrom(_ => _.Role));
    }
}