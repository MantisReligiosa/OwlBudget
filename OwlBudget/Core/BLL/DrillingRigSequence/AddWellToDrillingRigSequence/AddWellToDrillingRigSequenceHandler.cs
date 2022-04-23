using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.DAL.RepositoryInterfaces;
using Core.Domain;
using MediatR;

namespace Core.BLL.DrillingRigSequence.AddWellToDrillingRigSequence;

public class AddWellToDrillingRigSequenceHandler : AsyncRequestHandler<AddWellToDrillingRigSequenceRequest>
{
    private readonly IDrillingRigSequenceRepository _drillingRigSequenceRepository;
    private readonly IWellsToDrillingRigSequencesRepository _wellsToDrillingRigSequencesRepository;

    public AddWellToDrillingRigSequenceHandler(IDrillingRigSequenceRepository drillingRigSequenceRepository,
        IWellsToDrillingRigSequencesRepository wellsToDrillingRigSequencesRepository)
    {
        _drillingRigSequenceRepository = drillingRigSequenceRepository;
        _wellsToDrillingRigSequencesRepository = wellsToDrillingRigSequencesRepository;
    }

    protected override async Task Handle(AddWellToDrillingRigSequenceRequest request,
        CancellationToken cancellationToken)
    {
        var sequence = await _drillingRigSequenceRepository.GetByIdAsync(request.SequenceId, cancellationToken);

        var newOrder = sequence.WellsToDrillingRigSequence.Any()
            ? sequence.WellsToDrillingRigSequence.Max(_ => _.DrillingOrder) + 1
            : 1;
        await _wellsToDrillingRigSequencesRepository.CreateAsync(new WellToDrillingRigSequence
            {
                DrillingOrder = newOrder,
                DrillingRigSequenceId = sequence.Id,
                WellId = request.WellId
            },
            cancellationToken);
    }
}