using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.BLL.DrillingRigSequence.AddWellToDrillingRigSequence;
using Core.BLL.DrillingRigSequence.CreateDrillingRigSequence;
using Core.DAL.RepositoryInterfaces;
using Core.Domain;
using MediatR;

namespace Core.BLL.Well.CreateWell;

public class CreateWellHandler : AsyncRequestHandler<CreateWellRequest>
{
    private readonly IDrillingRigRepository _drillingRigRepository;
    private readonly IDrillingRigSequenceRepository _drillingRigSequenceRepository;
    private readonly IMediator _mediator;
    private readonly IProjectRepository _projectRepository;
    private readonly IScenarioRepository _scenarioRepository;
    private readonly IWellConstructionRepository _wellConstructionRepository;
    private readonly IWellRepository _wellRepository;

    public CreateWellHandler(IMediator mediator,
        IWellRepository wellRepository,
        IWellConstructionRepository wellConstructionRepository,
        IDrillingRigRepository drillingRigRepository,
        IProjectRepository projectRepository,
        IScenarioRepository scenarioRepository,
        IDrillingRigSequenceRepository drillingRigSequenceRepository
    )
    {
        _mediator = mediator;
        _wellRepository = wellRepository;
        _wellConstructionRepository = wellConstructionRepository;
        _drillingRigRepository = drillingRigRepository;
        _projectRepository = projectRepository;
        _scenarioRepository = scenarioRepository;
        _drillingRigSequenceRepository = drillingRigSequenceRepository;
    }

    protected override async Task Handle(CreateWellRequest request, CancellationToken cancellationToken)
    {
        var construction = await _wellConstructionRepository.FirstAsync(cancellationToken);
        var drillingRig = await _drillingRigRepository.FirstAsync(cancellationToken);
        var project = await _projectRepository.GetByClusterIdAsync(request.ClusterId, cancellationToken);
        var scenarios = await _scenarioRepository.GetProjectScenariosAsync(project.Id, cancellationToken);
        var well = await _wellRepository.CreateAsync(new Domain.Well
            {
                ClusterId = request.ClusterId,
                Name = "Скважина",
                ConstructionId = construction.Id,
                WellsToDrillingRigSequences = new List<WellToDrillingRigSequence>()
            },
            cancellationToken);
        foreach (var scenarioId in scenarios.Select(scenario => scenario.Id))
        {
            var sequence =
                await _drillingRigSequenceRepository.GetSequenceAsync(scenarioId, drillingRig.Id, cancellationToken) ??
                await _mediator.Send(new CreateDrillingRigSequenceRequest(drillingRig.Id, scenarioId),
                    cancellationToken);
            await _mediator.Send(new AddWellToDrillingRigSequenceRequest(sequence.Id, well.Id), cancellationToken);
        }
    }
}