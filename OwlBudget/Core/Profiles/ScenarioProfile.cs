using AutoMapper;
using Core.DAL.Entities;
using Core.Domain;

namespace Core.Profiles;

public class ScenarioProfile : Profile
{
    public ScenarioProfile()
    {
        CreateMap<Scenario, ScenarioEntity>();
        CreateMap<ScenarioEntity, Scenario>();
    }
}