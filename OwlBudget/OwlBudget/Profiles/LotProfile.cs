using AutoMapper;
using Core.Domain;

namespace OwlBudget.Profiles;

public class LotProfile : Profile
{
    public LotProfile()
    {
        CreateMap<Lot, Models.ProjectObjectsModels.Lot>();
    }
}