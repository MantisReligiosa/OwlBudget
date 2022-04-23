using Core.DAL.RepositoryInterfaces;
using Core.Domain;
using Core.ServiceInterfaces;

namespace Core.Services;

public class RegionService : BaseCatalogService<Region>, IRegionService
{
    public RegionService(IRegionRepository regionRepository)
        : base(regionRepository)
    {
    }
}