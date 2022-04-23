using AutoMapper;
using Core.DAL.Entities;
using Core.Domain;

namespace Core.Profiles;

public class RegionProfile : Profile
{
    public RegionProfile()
    {
        CreateMap<RegionEntity, Region>();
        CreateMap<Region, RegionEntity>();
    }
}