using AutoMapper;
using Core.DAL.Entities;
using Core.Domain;

namespace Core.Profiles;

public class ClusterProfile : Profile
{
    public ClusterProfile()
    {
        CreateMap<ClusterEntity, Cluster>()
            .ForMember(_ => _.Type, _ => _.MapFrom(m => m.WellType))
            .ForMember(_ => _.TypeId, _ => _.MapFrom(m => m.WellTypeId));

        CreateMap<Cluster, ClusterEntity>()
            .ForMember(_ => _.Lot, _ => _.Ignore())
            .ForMember(_ => _.WellType, _ => _.Ignore())
            .ForMember(_ => _.WellTypeId, _ => _.MapFrom(m => m.TypeId));
    }
}