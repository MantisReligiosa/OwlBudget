using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.DAL.RepositoryInterfaces;
using MediatR;

namespace Core.BLL.Scenario.GetProjectScenarios;

public class GetProjectScenariosHandler : IRequestHandler<GetProjectScenariosRequest, List<Domain.Scenario>>
{
    private readonly IScenarioRepository _scenarioRepository;

    public GetProjectScenariosHandler(IScenarioRepository scenarioRepository)
    {
        _scenarioRepository = scenarioRepository;
    }

    public async Task<List<Domain.Scenario>> Handle(GetProjectScenariosRequest request,
        CancellationToken cancellationToken)
    {
        return await _scenarioRepository.GetProjectScenariosAsync(request.ProjectId, cancellationToken);
    }
}