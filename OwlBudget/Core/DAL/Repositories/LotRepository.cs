using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Entities;
using Core.DAL.RepositoryInterfaces;
using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Core.DAL.Repositories;

public class LotRepository : Repository<Lot, LotEntity>, ILotRepository
{
    public LotRepository(IDatabaseContext context, IMapper mapper)
        : base(context, mapper)
    {
    }

    public async Task<List<Lot>> GetProjectLotsAsync(Guid projectId, CancellationToken cancellationToken)
    {
        var query = _context.Get<LotEntity>(_ => _.ProjectId == projectId)
            .Include(_ => _.Clusters)
            .ThenInclude(_ => _.Wells)
            .ThenInclude(_ => _.WellsToDrillingRigSequences)
            .ThenInclude(_ => _.DrillingRigSequence)
            .ThenInclude(_ => _.DrillingRig);
        var entities = await query
            .ToListAsync(cancellationToken);
        var result = _mapper.Map<List<Lot>>(entities);
        return result;
    }

    public override async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Get<LotEntity>()
            .Include(_ => _.Clusters)
            .ThenInclude(_ => _.Wells)
            .ThenInclude(_ => _.WellsToDrillingRigSequences)
            .SingleOrDefaultAsync(_ => _.Id == id, cancellationToken);
        if (entity != null)
        {
            _context.RemoveEntity(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}