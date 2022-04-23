using AutoMapper;
using Core.DAL.Entities;
using Core.Domain;

namespace Core.Profiles;

public class DrillingRigSequenceProfile : Profile
{
    public DrillingRigSequenceProfile()
    {
        CreateMap<DrillingRigSequenceEntity, DrillingRigSequence>();

        CreateMap<DrillingRigSequence, DrillingRigSequenceEntity>()
            .ForMember(_ => _.DrillingRig, _ => _.Ignore())
            .ForMember(_ => _.Scenario, _ => _.Ignore());
    }
}