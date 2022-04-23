using AutoMapper;
using Core.BLL.Well.GetWellsHeaders;

namespace OwlBudget.Profiles;

public class WellHeaderProfile : Profile
{
    public WellHeaderProfile()
    {
        CreateMap<WellHeader, Models.ProjectObjectsModels.WellHeader>();
    }
}