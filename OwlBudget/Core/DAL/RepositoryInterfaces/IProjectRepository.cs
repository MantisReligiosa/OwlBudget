using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain;

namespace Core.DAL.RepositoryInterfaces;

public interface IProjectRepository : IRepository<Project>
{
    Task<bool> IsNameInUseAsync(string name, Guid currentProjectId, CancellationToken cancellationToken);
    Task<Project> GetByClusterIdAsync(Guid clusterId, CancellationToken cancellationToken);
}