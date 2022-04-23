using AutoMapper;
using Core.DAL.Entities;
using Core.DAL.RepositoryInterfaces;
using Core.Domain;

namespace Core.DAL.Repositories;

public class WellConstrustionRepository : Repository<WellConstruction, WellConstructionEntity>,
    IWellConstructionRepository
{
    public WellConstrustionRepository(IDatabaseContext context, IMapper mapper)
        : base(context, mapper)
    {
    }
}