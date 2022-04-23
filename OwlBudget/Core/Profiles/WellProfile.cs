using AutoMapper;
using Core.DAL.Entities;
using Core.Domain;

namespace Core.Profiles;

public class WellProfile : Profile
{
    public WellProfile()
    {
        CreateMap<Well, WellEntity>()
            .ForMember(_ => _.WellsToDrillingRigSequences, _ => _.MapFrom(m => m.WellsToDrillingRigSequences))
            .ForMember(_ => _.WellBuildingScheduleItems, _ => _.MapFrom(m => m.WellBuildingScheduleItems));
        CreateMap<WellEntity, Well>()
            .ForMember(_ => _.WellsToDrillingRigSequences, _ => _.MapFrom(m => m.WellsToDrillingRigSequences));
    }
}