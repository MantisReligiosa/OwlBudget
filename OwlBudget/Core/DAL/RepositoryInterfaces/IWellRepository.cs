using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain;

namespace Core.DAL.RepositoryInterfaces;

public interface IWellRepository : IRepository<Well>
{
    Task<List<Well>> GetForProject(Guid projectId, CancellationToken cancellationToken);
}