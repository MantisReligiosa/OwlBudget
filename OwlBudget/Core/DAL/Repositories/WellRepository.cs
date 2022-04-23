using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Entities;
using Core.DAL.RepositoryInterfaces;
using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Core.DAL.Repositories;

public class WellRepository : Repository<Well, WellEntity>, IWellRepository
{
    public WellRepository(IDatabaseContext context, IMapper mapper)
        : base(context, mapper)
    {
    }

    public override async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Get<WellEntity>()
            .Include(_ => _.WellsToDrillingRigSequences)
            .SingleOrDefaultAsync(_ => _.Id == id, cancellationToken);
        if (entity != null)
        {
            _context.RemoveEntity(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<List<Well>> GetForProject(Guid projectId, CancellationToken cancellationToken)
    {
        var entities = await _context.Get<WellEntity>()
            .Include(_ => _.Cluster)
            .ThenInclude(_ => _.Lot)
            .Where(_ => _.Cluster.Lot.ProjectId == projectId)
            .ToListAsync(cancellationToken);
        return _mapper.Map<List<Well>>(entities);
    }
}