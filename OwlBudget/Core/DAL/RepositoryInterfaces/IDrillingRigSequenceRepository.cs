using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain;

namespace Core.DAL.RepositoryInterfaces;

public interface IDrillingRigSequenceRepository : IRepository<DrillingRigSequence>
{
    Task<DrillingRigSequence>
        GetSequenceAsync(Guid scenarioId, Guid drillingRigId, CancellationToken cancellationToken);

    Task<List<DrillingRigSequence>> GetSequencesByWellIdAsync(Guid wellId, CancellationToken cancellationToken);
    Task<List<DrillingRigSequence>> GetProjectSequencesAsync(Guid projectId, CancellationToken cancellationToken);
}