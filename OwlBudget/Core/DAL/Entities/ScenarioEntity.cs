using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.DAL.Entities;

[Table("Scenarios")]
public class ScenarioEntity : NamedEntity
{
    [Column("ProjectId")] public Guid ProjectId { get; set; }

    [Column("BudgetType")] public int BudgetType { get; set; }

    public ProjectEntity Project { get; set; }
}