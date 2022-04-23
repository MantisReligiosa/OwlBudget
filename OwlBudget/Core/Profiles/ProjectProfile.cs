using AutoMapper;
using Core.DAL.Entities;
using Core.Domain;

namespace Core.Profiles;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<ProjectEntity, Project>()
            .ForMember(_ => _.UserId, _ => _.MapFrom(m => m.OwnerId))
            .ForMember(_ => _.User, _ => _.MapFrom(m => m.Owner))
            .ForMember(_ => _.ContractType, _ => _.MapFrom(m => m.ContractTypeId))
            .ForMember(_ => _.Lots, _ => _.MapFrom(m => m.Lots));
        CreateMap<Project, ProjectEntity>()
            .ForMember(_ => _.OwnerId, _ => _.MapFrom(m => m.UserId))
            .ForMember(_ => _.Owner, _ => _.Ignore())
            .ForMember(_ => _.ContractTypeId, _ => _.MapFrom(m => m.ContractType))
            .ForMember(_ => _.Lots, _ => _.MapFrom(m => m.Lots))
            .ForMember(_ => _.Region, _ => _.Ignore())
            .ForMember(_ => _.CreatedEpoch, _ => _.Ignore())
            .ForMember(_ => _.ModificationEpoch, _ => _.Ignore());
    }
}