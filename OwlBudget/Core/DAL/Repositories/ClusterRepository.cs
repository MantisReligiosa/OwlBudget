using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Entities;
using Core.DAL.RepositoryInterfaces;
using Core.DAL.Specifications;
using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Core.DAL.Repositories;

public class ClusterRepository : Repository<Cluster, ClusterEntity>, IClusterRepository
{
    public ClusterRepository(IDatabaseContext context, IMapper mapper)
        : base(context, mapper)
    {
    }

    public override async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Get<ClusterEntity>()
            .Include(_ => _.Wells)
            .ThenInclude(_ => _.WellsToDrillingRigSequences)
            .SingleOrDefaultAsync(EntitySpecification.ById(id), cancellationToken);
        if (entity != null)
        {
            _context.RemoveEntity(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<List<Cluster>> GetAllByProjectIdAsync(Guid projectId, CancellationToken cancellationToken)
    {
        var entities = await _context.Get<ClusterEntity>()
            .Include(_ => _.Lot)
            .Include(_ => _.WellType)
            .Where(_ => _.Lot.ProjectId.Equals(projectId))
            .ToListAsync(cancellationToken);
        return _mapper.Map<List<Cluster>>(entities);
    }
}