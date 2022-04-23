using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.BLL.Scenario.CreateScenario;
using Core.DAL.RepositoryInterfaces;
using Core.Domain;
using Core.ServiceInterfaces;
using MediatR;

namespace Core.BLL.Project.CreateProject;

public class CreateProjectHandler : IRequestHandler<CreateProjectRequest, Domain.Project>
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IMediator _mediator;
    private readonly IProjectRepository _projectRepository;
    private readonly IRegionRepository _regionRepository;

    public CreateProjectHandler(IMediator mediator,
        IDateTimeProvider dateTimeProvider,
        IRegionRepository regionRepository,
        IProjectRepository projectRepository)
    {
        _mediator = mediator;
        _dateTimeProvider = dateTimeProvider;
        _regionRepository = regionRepository;
        _projectRepository = projectRepository;
    }

    public async Task<Domain.Project> Handle(CreateProjectRequest request, CancellationToken cancellationToken)
    {
        var currentDateTime = _dateTimeProvider.GetCurrentDatetime();
        var region = await _regionRepository.FirstAsync(cancellationToken);
        var project = await _projectRepository.CreateAsync(new Domain.Project
            {
                Name = "Новый проект",
                UserId = request.UserId,
                ContractType = ContractTypes.TurnKey,
                CreatedDt = currentDateTime,
                ModifiedDt = currentDateTime,
                Region = region,
                RegionId = region.Id,
                Scenarios = new List<Domain.Scenario>()
            },
            cancellationToken);
        for (var budgetType = 1; budgetType <= 3; budgetType++)
            await _mediator.Send(new CreateScenarioRequest(project.Id, budgetType), cancellationToken);
        var result = await _projectRepository.GetByIdAsync(project.Id, cancellationToken);
        return result;
    }
}