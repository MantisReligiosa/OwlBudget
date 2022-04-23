using System.Threading;
using System.Threading.Tasks;
using Core.DAL.RepositoryInterfaces;
using MediatR;

namespace Core.BLL.WellBuildingSchedule.DeleteWellBuildingScheduleItem;

public class DeleteWellBuildingScheduleItemHandler : AsyncRequestHandler<DeleteWellBuildingScheduleItemRequest>
{
    private readonly IWellBuildingScheduleRepository _wellBuildingScheduleRepository;

    public DeleteWellBuildingScheduleItemHandler(IWellBuildingScheduleRepository wellBuildingScheduleRepository)
    {
        _wellBuildingScheduleRepository = wellBuildingScheduleRepository;
    }

    protected override async Task Handle(DeleteWellBuildingScheduleItemRequest request,
        CancellationToken cancellationToken)
    {
        await _wellBuildingScheduleRepository.DeleteByIdAsync(request.ItemId, cancellationToken);
    }
}