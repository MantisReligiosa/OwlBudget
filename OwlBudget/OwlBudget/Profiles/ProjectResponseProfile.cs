using AutoMapper;
using Core.Domain;
using OwlBudget.Models;

namespace OwlBudget.Profiles;

public class ProjectResponseProfile : Profile
{
    public ProjectResponseProfile()
    {
        CreateMap<Project, ProjectParamsResponse>()
            .ForMember(_ => _.AccessMode, _ => _.MapFrom(m => AccessMode.Owner))
            .ForMember(_ => _.ContractTypeId, _ => _.MapFrom(m => m.ContractType));
    }
}