using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Entities;
using Core.DAL.RepositoryInterfaces;
using Core.Domain;

namespace Core.DAL.Repositories;

public class ScenarioRepository : Repository<Scenario, ScenarioEntity>, IScenarioRepository
{
    public ScenarioRepository(IDatabaseContext context, IMapper mapper)
        : base(context, mapper)
    {
    }

    public async Task<List<Scenario>> GetProjectScenariosAsync(Guid projectId, CancellationToken cancellationToken)
    {
        var entities = await GetAsync(_ => _.ProjectId == projectId, cancellationToken);
        var result = _mapper.Map<List<Scenario>>(entities);
        return result;
    }
}