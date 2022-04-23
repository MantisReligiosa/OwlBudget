using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain;

namespace Core.DAL.RepositoryInterfaces;

public interface ILotRepository : IRepository<Lot>
{
    Task<List<Lot>> GetProjectLotsAsync(Guid projectId, CancellationToken cancellationToken);
}