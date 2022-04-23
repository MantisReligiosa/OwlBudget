using AutoMapper;
using Core.DAL.Entities;
using Core.Domain;

namespace Core.Profiles;

public class WellToDrillingRigSequenceProfile : Profile
{
    public WellToDrillingRigSequenceProfile()
    {
        CreateMap<WellToDrillingRigSequence, WellToDrillingRigSequenceEntity>();
        CreateMap<WellToDrillingRigSequenceEntity, WellToDrillingRigSequence>();
    }
}