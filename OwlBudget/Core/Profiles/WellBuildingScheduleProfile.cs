using AutoMapper;
using Core.DAL.Entities;
using Core.Domain;

namespace Core.Profiles;

public class WellBuildingScheduleProfile : Profile
{
    public WellBuildingScheduleProfile()
    {
        CreateMap<WellBuildingScheduleItem, WellBuildingScheduleItemEntity>();
        CreateMap<WellBuildingScheduleItemEntity, WellBuildingScheduleItem>();
    }
}