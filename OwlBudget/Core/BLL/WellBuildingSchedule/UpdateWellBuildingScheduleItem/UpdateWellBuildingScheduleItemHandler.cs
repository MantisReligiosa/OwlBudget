using System.Threading;
using System.Threading.Tasks;
using Core.DAL.RepositoryInterfaces;
using MediatR;
using static Core.Domain.ConstructionScheduleTemplates;

namespace Core.BLL.WellBuildingSchedule.UpdateWellBuildingScheduleItem;

public class UpdateWellBuildingScheduleItemHandler : AsyncRequestHandler<UpdateWellBuildingScheduleItemRequest>
{
    private readonly IWellBuildingScheduleRepository _wellBuildingScheduleRepository;

    public UpdateWellBuildingScheduleItemHandler(IWellBuildingScheduleRepository wellBuildingScheduleRepository)
    {
        _wellBuildingScheduleRepository = wellBuildingScheduleRepository;
    }

    protected override async Task Handle(UpdateWellBuildingScheduleItemRequest request,
        CancellationToken cancellationToken)
    {
        var schedule = await _wellBuildingScheduleRepository.GetByIdAsync(request.Id, cancellationToken);
        schedule.DurationDays = request.DurationDays;

        await _wellBuildingScheduleRepository.UpdateAsync(schedule, cancellationToken);
    }
}