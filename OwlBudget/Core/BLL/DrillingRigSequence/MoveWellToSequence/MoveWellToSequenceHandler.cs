using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.DAL.RepositoryInterfaces;
using Core.Domain;
using MediatR;

namespace Core.BLL.DrillingRigSequence.MoveWellToSequence;

public class MoveWellToSequenceHandler : AsyncRequestHandler<MoveWellToSequenceRequest>
{
    private readonly IDrillingRigSequenceRepository _drillingRigSequenceRepository;
    private readonly IWellsToDrillingRigSequencesRepository _wellsToDrillingRigSequencesRepository;

    public MoveWellToSequenceHandler(IWellsToDrillingRigSequencesRepository wellsToDrillingRigSequencesRepository,
        IDrillingRigSequenceRepository drillingRigSequenceRepository)
    {
        _wellsToDrillingRigSequencesRepository = wellsToDrillingRigSequencesRepository;
        _drillingRigSequenceRepository = drillingRigSequenceRepository;
    }

    public async Task HandleIt(MoveWellToSequenceRequest request, CancellationToken cancellationToken)
    {
        await Handle(request, cancellationToken);
    }

    protected override async Task Handle(MoveWellToSequenceRequest request, CancellationToken cancellationToken)
    {
        var currentItem =
            await _wellsToDrillingRigSequencesRepository.GetByWellAndSequenceAsync(request.WellId,
                request.CurrentSequenceId, cancellationToken);

        if (currentItem == null)
            return;

        //В текущей последовательности сдвинуть влево все элементы правее текущего
        ChangeSequenceItemsOrder(
            await _wellsToDrillingRigSequencesRepository.GetByScequenceIdAsync(request.CurrentSequenceId,
                cancellationToken),
            currentItem.DrillingOrder,
            -1);

        //В новой последовательности сдвинуть вправо все элементы правее текущего
        ChangeSequenceItemsOrder(
            await _wellsToDrillingRigSequencesRepository.GetByScequenceIdAsync(request.NewSequenceId,
                cancellationToken),
            request.Order);

        //Изменить sequenceId и order текущего элемента
        currentItem.DrillingRigSequenceId = request.NewSequenceId;
        currentItem.DrillingOrder = request.Order;
        await _wellsToDrillingRigSequencesRepository.UpdateAsync(currentItem, cancellationToken);

        //Если текущая последовательность окажется пустая - удалить её
        if (!(await _wellsToDrillingRigSequencesRepository.GetByScequenceIdAsync(request.CurrentSequenceId,
                cancellationToken))
            .Any()) await _drillingRigSequenceRepository.DeleteByIdAsync(request.CurrentSequenceId, cancellationToken);
    }

    private void ChangeSequenceItemsOrder(IEnumerable<WellToDrillingRigSequence> items, int startOrder,
        int increment = 1)
    {
        foreach (var item in items.Where(_ => _.DrillingOrder >= startOrder))
        {
            item.DrillingOrder += increment;
            _wellsToDrillingRigSequencesRepository.Update(item);
        }
    }
}