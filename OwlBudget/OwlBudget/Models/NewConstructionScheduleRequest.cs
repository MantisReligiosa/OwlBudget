using System;

namespace OwlBudget.Models;

public class NewConstructionScheduleRequest
{
    public Guid ProjectId { get; set; }
    public Guid ScenarioId { get; set; }
    public Guid WellId { get; set; }
    public int TemplateType { get; set; }
}