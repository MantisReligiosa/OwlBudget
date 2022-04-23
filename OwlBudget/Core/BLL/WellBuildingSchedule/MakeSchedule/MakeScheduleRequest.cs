using System;
using System.Collections.Generic;
using MediatR;

namespace Core.BLL.WellBuildingSchedule.MakeSchedule;

public record MakeScheduleRequest(Guid ProjectId) : IRequest<List<Dictionary<string, object>>>;