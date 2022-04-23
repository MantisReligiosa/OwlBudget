using AutoMapper;
using Core.Domain;

namespace OwlBudget.Profiles;

public class DrillingRigSequenceProfile : Profile
{
    public DrillingRigSequenceProfile()
    {
        CreateMap<DrillingRigSequence, Models.ProjectObjectsModels.DrillingRigSequence>();
    }
}