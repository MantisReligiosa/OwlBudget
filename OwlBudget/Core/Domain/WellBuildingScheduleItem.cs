using System;

namespace Core.Domain;

public class WellBuildingScheduleItem : Identity
{
    public Guid ScenarioId { get; set; }

    public Scenario Scenario { get; set; }

    public ConstructionScheduleTemplates.ConstructionScheduleTemplateTypes TemplateType { get; set; }

    public Guid WellId { get; set; }

    public Well Well { get; set; }
    
    public int? DurationDays { get; set; }
}