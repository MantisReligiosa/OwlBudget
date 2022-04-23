using System.Threading;
using System.Threading.Tasks;
using Core.DAL.RepositoryInterfaces;
using Core.Domain;
using Core.ServiceInterfaces;
using MediatR;
using static Core.Domain.ConstructionScheduleTemplates;

namespace Core.BLL.WellBuildingSchedule.CreateWellBuildingScheduleItem;

public class CreateWellBuildingScheduleItemHandler : AsyncRequestHandler<CreateWellBuildingScheduleItemRequest>
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IWellBuildingScheduleRepository _wellBuildingScheduleRepository;

    public CreateWellBuildingScheduleItemHandler(IWellBuildingScheduleRepository wellBuildingScheduleRepository,
        IDateTimeProvider dateTimeProvider)
    {
        _wellBuildingScheduleRepository = wellBuildingScheduleRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    protected override async Task Handle(CreateWellBuildingScheduleItemRequest request,
        CancellationToken cancellationToken)
    {
        var templateType = ByType((ConstructionScheduleTemplateTypes) request.TemplateType);
        var wellBuildingScheduleItem = new WellBuildingScheduleItem
        {
            ScenarioId = request.ScenarioId,
            TemplateType = templateType.TemplateType,
            WellId = request.WellId,
        };

        if (!templateType.HaveVariableLatency)
            wellBuildingScheduleItem.DurationDays = 1;
        //#error Надо как-то выстраивать все графики по порядку при операциях с графиками

        await _wellBuildingScheduleRepository.CreateAsync(wellBuildingScheduleItem, cancellationToken);
    }
}