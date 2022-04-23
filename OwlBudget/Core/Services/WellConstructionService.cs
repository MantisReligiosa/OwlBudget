using Core.DAL.RepositoryInterfaces;
using Core.Domain;
using Core.ServiceInterfaces;

namespace Core.Services;

public class WellConstructionService : BaseCatalogService<WellConstruction>, IWellConstructionService
{
    public WellConstructionService(IWellConstructionRepository wellTypeRepository)
        : base(wellTypeRepository)
    {
    }
}