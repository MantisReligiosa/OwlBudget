using System;
using MediatR;

namespace Core.BLL.WellBuildingSchedule.DeleteWellBuildingScheduleItem;

public record DeleteWellBuildingScheduleItemRequest(Guid ItemId) : IRequest;