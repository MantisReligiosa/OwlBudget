using AutoMapper;
using Core.Domain;
using OwlBudget.Models.TableResponse;

namespace OwlBudget.Profiles;

public class CatalogProfile : Profile
{
    public CatalogProfile()
    {
        CreateMap<Region, CatalogItem>();
        CreateMap<WellConstruction, CatalogItem>();
        CreateMap<WellType, CatalogItem>();
        CreateMap<DrillingRig, CatalogItem>();
        CreateMap<Project, ProjectCatalogItem>()
            .ForMember(_ => _.Created, _ => _.MapFrom(m => m.CreatedDt))
            .ForMember(_ => _.Modified, _ => _.MapFrom(m => m.ModifiedDt));
    }
}