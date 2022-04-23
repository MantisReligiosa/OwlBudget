using System;

namespace OwlBudget.Models;

public record UpdateScheduleRequest(Guid Id, int TemplateType, int DurationDays);