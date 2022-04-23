using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Tools;

namespace Core.DAL.Entities;

[Table("WellBuildingSchedules")]
public class WellBuildingScheduleItemEntity : Entity
{
    [Column("ScenarioId")] public Guid ScenarioId { get; set; }

    [Column("TemplateType")] public int TemplateType { get; set; }

    [Column("WellId")] public Guid WellId { get; set; }
    
    [Column("DurationDays")] public int? DurationDays { get; set; }

    public WellEntity Well { get; set; }

    public ScenarioEntity Scenario { get; set; }
}