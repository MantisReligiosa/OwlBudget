using System;
using MediatR;

namespace Core.BLL.WellBuildingSchedule.CreateWellBuildingScheduleItem;

public record CreateWellBuildingScheduleItemRequest
    (Guid ProjectId, Guid ScenarioId, Guid WellId, int TemplateType) : IRequest;