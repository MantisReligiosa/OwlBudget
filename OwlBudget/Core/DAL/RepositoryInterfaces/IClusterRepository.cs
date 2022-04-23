using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain;

namespace Core.DAL.RepositoryInterfaces;

public interface IClusterRepository : IRepository<Cluster>
{
    Task<List<Cluster>> GetAllByProjectIdAsync(Guid projectId, CancellationToken cancellationToken);
}