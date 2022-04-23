using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.DAL.Entities;

[Table("DepthDayGraphs")]
public class DepthDayGraphDurationItemEntity : Entity
{
    [Column("WellTypeId")] public Guid WellTypeId { get; set; }

    public WellTypeEntity WellType { get; set; }

    public int Day { get; set; }
    public double Depth { get; set; }
}