using AutoMapper;

namespace OwlBudget.Profiles;

public class WellToDrillingRigSequence : Profile
{
    public WellToDrillingRigSequence()
    {
        CreateMap<Core.Domain.WellToDrillingRigSequence, Models.ProjectObjectsModels.WellToDrillingRigSequence>();
    }
}