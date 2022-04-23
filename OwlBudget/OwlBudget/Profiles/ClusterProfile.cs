using AutoMapper;
using Core.Domain;

namespace OwlBudget.Profiles;

public class ClusterProfile : Profile
{
    public ClusterProfile()
    {
        CreateMap<Cluster, Models.ProjectObjectsModels.Cluster>();
    }
}