using System.Threading;
using System.Threading.Tasks;
using Core.DAL.RepositoryInterfaces;
using MediatR;

namespace Core.BLL.Scenario.CreateScenario;

public class CreateScenarioHandler : AsyncRequestHandler<CreateScenarioRequest>
{
    private readonly IScenarioRepository _scenarioRepository;

    public CreateScenarioHandler(IScenarioRepository scenarioRepository)
    {
        _scenarioRepository = scenarioRepository;
    }

    protected override async Task Handle(CreateScenarioRequest request, CancellationToken cancellationToken)
    {
        await _scenarioRepository.CreateAsync(new Domain.Scenario
            {
                BudgetType = request.BudgetType,
                Name = "Сценарий",
                ProjectId = request.ProjectId
            },
            cancellationToken);
    }
}