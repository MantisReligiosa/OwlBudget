using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain;

namespace Core.DAL.RepositoryInterfaces;

public interface IScenarioRepository : IRepository<Scenario>
{
    Task<List<Scenario>> GetProjectScenariosAsync(Guid projectId, CancellationToken cancellationToken);
}