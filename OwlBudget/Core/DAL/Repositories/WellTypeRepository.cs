using AutoMapper;
using Core.DAL.Entities;
using Core.DAL.RepositoryInterfaces;
using Core.Domain;

namespace Core.DAL.Repositories;

public class WellTypeRepository : Repository<WellType, WellTypeEntity>, IWellTypeRepository
{
    public WellTypeRepository(IDatabaseContext context, IMapper mapper)
        : base(context, mapper)
    {
    }
}