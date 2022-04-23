using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.DAL.RepositoryInterfaces;
using MediatR;

namespace Core.BLL.DrillingRigSequence.ExcludeWellFromDrillingRigSequence;

public class ExcludeWellFromDrillingRigSequenceHandler : AsyncRequestHandler<ExcludeWellFromDrillingRigSequenceRequest>
{
    private readonly IDrillingRigSequenceRepository _drillingRigSequenceRepository;
    private readonly IWellsToDrillingRigSequencesRepository _wellsToDrillingRigSequencesRepository;

    public ExcludeWellFromDrillingRigSequenceHandler(
        IWellsToDrillingRigSequencesRepository wellsToDrillingRigSequencesRepository,
        IDrillingRigSequenceRepository drillingRigSequenceRepository)
    {
        _wellsToDrillingRigSequencesRepository = wellsToDrillingRigSequencesRepository;
        _drillingRigSequenceRepository = drillingRigSequenceRepository;
    }

    protected override async Task Handle(ExcludeWellFromDrillingRigSequenceRequest request,
        CancellationToken cancellationToken)
    {
        var sequence = await _drillingRigSequenceRepository.GetByIdAsync(request.SequenceId, cancellationToken);
        if (sequence.WellsToDrillingRigSequence.Count == 1)
        {
            await _drillingRigSequenceRepository.DeleteByIdAsync(sequence.Id, cancellationToken);
            return;
        }

        var itemToDelete = sequence.WellsToDrillingRigSequence.Single(_ => _.WellId == request.WellId);
        await _wellsToDrillingRigSequencesRepository.DeleteByIdAsync(itemToDelete.Id, cancellationToken);
        foreach (var itemIdsToReduceOrder in sequence.WellsToDrillingRigSequence
                     .Where(_ => _.DrillingOrder > itemToDelete.DrillingOrder).Select(_ => _.Id))
        {
            var item = await _wellsToDrillingRigSequencesRepository.GetByIdAsync(itemIdsToReduceOrder,
                cancellationToken);
            item.DrillingOrder--;
            await _wellsToDrillingRigSequencesRepository.UpdateAsync(item, cancellationToken);
        }
    }
}