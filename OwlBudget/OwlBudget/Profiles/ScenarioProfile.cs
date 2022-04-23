using AutoMapper;
using Core.Domain;

namespace OwlBudget.Profiles;

public class ScenarioProfile : Profile
{
    public ScenarioProfile()
    {
        CreateMap<Scenario, Models.ProjectObjectsModels.Scenario>();
    }
}