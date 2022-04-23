using AutoMapper;
using Core.DAL.Entities;
using Core.Domain;

namespace Core.Profiles;

public class DrillingRigProfile : Profile
{
    public DrillingRigProfile()
    {
        CreateMap<DrillingRig, DrillingRigEntity>()
            .ForMember(_ => _.DrillingRigSequences, _ => _.Ignore());
        CreateMap<DrillingRigEntity, DrillingRig>();
    }
}