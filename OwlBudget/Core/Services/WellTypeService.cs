using Core.DAL.RepositoryInterfaces;
using Core.Domain;
using Core.ServiceInterfaces;

namespace Core.Services;

public class WellTypeService : BaseCatalogService<WellType>, IWellTypeService
{
    public WellTypeService(IWellTypeRepository wellTypeRepository)
        : base(wellTypeRepository)
    {
    }
}