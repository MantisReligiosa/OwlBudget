using System;
using MediatR;
using static Core.Domain.ConstructionScheduleTemplates;

namespace Core.BLL.WellBuildingSchedule.UpdateWellBuildingScheduleItem;

public record UpdateWellBuildingScheduleItemRequest(Guid Id, int DurationDays,
    ConstructionScheduleTemplateType TemplateType) : IRequest;