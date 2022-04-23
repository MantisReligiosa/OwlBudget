using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain;

namespace Core.DAL.RepositoryInterfaces;

public interface IWellsToDrillingRigSequencesRepository : IRepository<WellToDrillingRigSequence>
{
    Task<WellToDrillingRigSequence> GetByWellAndScenarioAsync(Guid wellId, Guid scenarioId,
        CancellationToken cancellationToken);

    Task<WellToDrillingRigSequence> GetByWellAndSequenceAsync(Guid wellId, Guid sequenceId,
        CancellationToken cancellationToken);

    Task<List<WellToDrillingRigSequence>> GetByScequenceIdAsync(Guid sequenceId, CancellationToken cancellationToken);
}