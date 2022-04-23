using AutoMapper;
using Core.DAL.Entities;
using Core.DAL.RepositoryInterfaces;
using Core.Domain;

namespace Core.DAL.Repositories;

public class RegionRepository : Repository<Region, RegionEntity>, IRegionRepository
{
    public RegionRepository(IDatabaseContext context, IMapper mapper)
        : base(context, mapper)
    {
    }
}