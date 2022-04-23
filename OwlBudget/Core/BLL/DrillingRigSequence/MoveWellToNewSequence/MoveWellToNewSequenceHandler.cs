using System.Threading;
using System.Threading.Tasks;
using Core.BLL.DrillingRigSequence.CreateDrillingRigSequence;
using Core.BLL.DrillingRigSequence.MoveWellToSequence;
using Core.DAL.RepositoryInterfaces;
using MediatR;

namespace Core.BLL.DrillingRigSequence.MoveWellToNewSequence;

public class MoveWellToNewSequenceHandler : AsyncRequestHandler<MoveWellToNewSequenceRequest>
{
    private readonly IDrillingRigSequenceRepository _drillingRigSequenceRepository;
    private readonly IMediator _mediator;
    private readonly IWellsToDrillingRigSequencesRepository _wellsToDrillingRigSequencesRepository;

    public MoveWellToNewSequenceHandler(IMediator mediator,
        IWellsToDrillingRigSequencesRepository wellsToDrillingRigSequencesRepository,
        IDrillingRigSequenceRepository drillingRigSequenceRepository)
    {
        _mediator = mediator;
        _wellsToDrillingRigSequencesRepository = wellsToDrillingRigSequencesRepository;
        _drillingRigSequenceRepository = drillingRigSequenceRepository;
    }

    protected override async Task Handle(MoveWellToNewSequenceRequest request, CancellationToken cancellationToken)
    {
        var currentBinding =
            await _wellsToDrillingRigSequencesRepository.GetByWellAndScenarioAsync(request.WellId, request.ScenarioId,
                cancellationToken);
        var currentSequence =
            await _drillingRigSequenceRepository.GetByIdAsync(currentBinding.DrillingRigSequenceId, cancellationToken);
        var newSequence =
            await _mediator.Send(
                new CreateDrillingRigSequenceRequest(currentSequence.DrillingRigId, request.ScenarioId),
                cancellationToken);
        await _mediator.Send(new MoveWellToSequenceRequest(currentSequence.Id, newSequence.Id, request.WellId, 1),
            cancellationToken);
    }
}