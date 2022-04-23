using AutoMapper;
using Core.DAL.Entities;
using Core.Domain;

namespace Core.Profiles;

public class WellConstructionProfile : Profile
{
    public WellConstructionProfile()
    {
        CreateMap<WellConstruction, WellConstructionEntity>();
        CreateMap<WellConstructionEntity, WellConstruction>();
    }
}