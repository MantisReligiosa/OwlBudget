using AutoMapper;
using Core.DAL.Entities;
using Core.DAL.RepositoryInterfaces;
using Core.Domain;

namespace Core.DAL.Repositories;

public class DrillingRigRepository : Repository<DrillingRig, DrillingRigEntity>, IDrillingRigRepository
{
    public DrillingRigRepository(IDatabaseContext context, IMapper mapper)
        : base(context, mapper)
    {
    }
}