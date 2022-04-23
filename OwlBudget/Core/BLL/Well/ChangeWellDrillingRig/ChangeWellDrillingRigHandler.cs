using System.Threading;
using System.Threading.Tasks;
using Core.BLL.DrillingRigSequence.AddWellToDrillingRigSequence;
using Core.BLL.DrillingRigSequence.CreateDrillingRigSequence;
using Core.BLL.DrillingRigSequence.ExcludeWellFromDrillingRigSequence;
using Core.DAL.RepositoryInterfaces;
using Core.Models.OperationResults;
using MediatR;

namespace Core.BLL.Well.ChangeWellDrillingRig;

public class ChangeWellDrillingRigHandler : IRequestHandler<ChangeWellDrillingRigRequest, OperationResult>
{
    private readonly IDrillingRigSequenceRepository _drillingRigSequenceRepository;
    private readonly IMediator _mediator;
    private readonly IWellsToDrillingRigSequencesRepository _wellsToDrillingRigSequencesRepository;

    public ChangeWellDrillingRigHandler(IMediator mediator,
        IWellsToDrillingRigSequencesRepository wellsToDrillingRigSequencesRepository,
        IDrillingRigSequenceRepository drillingRigSequenceRepository)
    {
        _mediator = mediator;
        _wellsToDrillingRigSequencesRepository = wellsToDrillingRigSequencesRepository;
        _drillingRigSequenceRepository = drillingRigSequenceRepository;
    }

    public async Task<OperationResult> Handle(ChangeWellDrillingRigRequest request, CancellationToken cancellationToken)
    {
        var binding =
            await _wellsToDrillingRigSequencesRepository.GetByWellAndScenarioAsync(request.WellId, request.ScenarioId,
                cancellationToken);

        if (binding == null) return new ErrorOperationResult {ErrorMessage = "Не удалось найти привязку"};

        var sequenceFrom =
            await _drillingRigSequenceRepository.GetByIdAsync(binding.DrillingRigSequenceId, cancellationToken);
        var sequenceTo =
            await _drillingRigSequenceRepository.GetSequenceAsync(request.ScenarioId, request.DrillingRigId,
                cancellationToken);
        if (sequenceFrom.WellsToDrillingRigSequence.Count == 1 && sequenceTo == null)
        {
            sequenceFrom.DrillingRigId = request.DrillingRigId;
            await _drillingRigSequenceRepository.UpdateAsync(sequenceFrom, cancellationToken);
            return new SuccessOperationResult();
        }

        if (sequenceTo == null)
            sequenceTo =
                await _mediator.Send(new CreateDrillingRigSequenceRequest(request.DrillingRigId, request.ScenarioId),
                    cancellationToken);
        await _mediator.Send(new ExcludeWellFromDrillingRigSequenceRequest(sequenceFrom.Id, request.WellId),
            cancellationToken);
        await _mediator.Send(new AddWellToDrillingRigSequenceRequest(sequenceTo.Id, request.WellId), cancellationToken);
        return new SuccessOperationResult();
    }
}