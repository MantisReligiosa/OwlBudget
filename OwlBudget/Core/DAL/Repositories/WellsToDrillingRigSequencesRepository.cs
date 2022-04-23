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

public class WellsToDrillingRigSequencesRepository :
    Repository<WellToDrillingRigSequence, WellToDrillingRigSequenceEntity>, IWellsToDrillingRigSequencesRepository
{
    public WellsToDrillingRigSequencesRepository(IDatabaseContext context, IMapper mapper)
        : base(context, mapper)
    {
    }

    public async Task<List<WellToDrillingRigSequence>> GetByScequenceIdAsync(Guid sequenceId,
        CancellationToken cancellationToken)
    {
        var entities = await _context.Get<WellToDrillingRigSequenceEntity>(_ => _.DrillingRigSequenceId == sequenceId)
            .ToListAsync(cancellationToken);
        return _mapper.Map<List<WellToDrillingRigSequence>>(entities);
    }

    public async Task<WellToDrillingRigSequence> GetByWellAndScenarioAsync(Guid wellId, Guid scenarioId,
        CancellationToken cancellationToken)
    {
        var entity = await _context
            .SingleOrDefaultAsync<WellToDrillingRigSequenceEntity>(
                _ => _.WellId == wellId && _.DrillingRigSequence.ScenarioId == scenarioId, cancellationToken);
        return _mapper.Map<WellToDrillingRigSequence>(entity);
    }

    public async Task<WellToDrillingRigSequence> GetByWellAndSequenceAsync(Guid wellId, Guid sequenceId,
        CancellationToken cancellationToken)
    {
        var entity = await _context
            .SingleOrDefaultAsync<WellToDrillingRigSequenceEntity>(
                _ => _.WellId == wellId && _.DrillingRigSequence.Id == sequenceId, cancellationToken);
        return _mapper.Map<WellToDrillingRigSequence>(entity);
    }
}