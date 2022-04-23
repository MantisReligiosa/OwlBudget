using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.BLL.DrillingRigSequence.ExcludeWellFromDrillingRigSequence;
using Core.DAL.RepositoryInterfaces;
using MediatR;

namespace Core.BLL.Well.DeleteWell;

public class DeleteWellHandler : AsyncRequestHandler<DeleteWellRequest>
{
    private readonly IDrillingRigSequenceRepository _drillingRigSequenceRepository;
    private readonly IMediator _mediator;
    private readonly IWellBuildingScheduleRepository _wellBuildingScheduleRepository;
    private readonly IWellRepository _wellRepository;

    public DeleteWellHandler(IMediator mediator,
        IWellRepository wellRepository,
        IDrillingRigSequenceRepository drillingRigSequenceRepository,
        IWellBuildingScheduleRepository wellBuildingScheduleRepository)
    {
        _mediator = mediator;
        _wellRepository = wellRepository;
        _drillingRigSequenceRepository = drillingRigSequenceRepository;
        _wellBuildingScheduleRepository = wellBuildingScheduleRepository;
    }

    protected override async Task Handle(DeleteWellRequest request, CancellationToken cancellationToken)
    {
        var sequences =
            await _drillingRigSequenceRepository.GetSequencesByWellIdAsync(request.WellId, cancellationToken);

        var tasks = sequences.Select(_ =>
            _mediator.Send(new ExcludeWellFromDrillingRigSequenceRequest(_.Id, request.WellId),
                cancellationToken));

        await Task.WhenAll(tasks);

        var schedules = await _wellBuildingScheduleRepository.GetByWellIdAsync(request.WellId, cancellationToken);
        await _wellBuildingScheduleRepository.DeleteManyAsync(schedules, cancellationToken);

        await _wellRepository.DeleteByIdAsync(request.WellId, cancellationToken);
    }
}