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

public class DrillingRigSequenceRepository : Repository<DrillingRigSequence, DrillingRigSequenceEntity>,
    IDrillingRigSequenceRepository
{
    public DrillingRigSequenceRepository(IDatabaseContext context, IMapper mapper)
        : base(context, mapper)
    {
    }

    public override async Task<DrillingRigSequence> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Get<DrillingRigSequenceEntity>()
            .Include(_ => _.WellsToDrillingRigSequence)
            .SingleOrDefaultAsync(EntitySpecification.ById(id), cancellationToken);
        var result = _mapper.Map<DrillingRigSequence>(entity);
        return result;
    }

    public async Task<List<DrillingRigSequence>> GetProjectSequencesAsync(Guid projectId,
        CancellationToken cancellationToken)
    {
        var entities = await _context.Get<DrillingRigSequenceEntity>()
            .Include(_ => _.DrillingRig)
            .Include(_ => _.WellsToDrillingRigSequence.OrderBy(_ => _.DrillingOrder))
            .ThenInclude(_ => _.Well)
            .Include(_ => _.Scenario)
            .Where(_ => _.Scenario.ProjectId == projectId)
            .OrderBy(_ => _.DrillingRigId)
            .ToListAsync(cancellationToken);
        var result = _mapper.Map<List<DrillingRigSequence>>(entities);
        return result;
    }

    public async Task<DrillingRigSequence> GetSequenceAsync(Guid scenarioId, Guid drillingRigId,
        CancellationToken cancellationToken)
    {
        var entity = await _context.Get<DrillingRigSequenceEntity>()
            .Include(_ => _.WellsToDrillingRigSequence)
            .SingleOrDefaultAsync(_ => _.ScenarioId == scenarioId && _.DrillingRigId == drillingRigId,
                cancellationToken);
        var result = _mapper.Map<DrillingRigSequence>(entity);
        return result;
    }

    public async Task<List<DrillingRigSequence>> GetSequencesByWellIdAsync(Guid wellId,
        CancellationToken cancellationToken)
    {
        var entity = await _context.Get<DrillingRigSequenceEntity>()
            .Include(_ => _.WellsToDrillingRigSequence)
            .Where(_ => _.WellsToDrillingRigSequence.Any(_ => _.WellId == wellId))
            .ToListAsync(cancellationToken);
        var result = _mapper.Map<List<DrillingRigSequence>>(entity);
        return result;
    }
}