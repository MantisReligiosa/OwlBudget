using System;

namespace Core.Domain;

public class DepthDayGraphDurationItem : Identity
{
    public Guid WellTypeId { get; set; }

    public WellType WellType { get; set; }

    public int Day { get; set; }
    public double Depth { get; set; }
}