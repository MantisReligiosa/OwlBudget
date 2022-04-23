using AutoMapper;
using Core.DAL.Entities;
using Core.Domain;

namespace Core.Profiles;

public class WellTypeProfile : Profile
{
    public WellTypeProfile()
    {
        CreateMap<WellType, WellTypeEntity>();
        CreateMap<WellTypeEntity, WellType>();
    }
}