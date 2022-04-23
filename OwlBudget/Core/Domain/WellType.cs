using System.Collections.Generic;

namespace Core.Domain;

public class WellType : NamedIdentity
{
    public List<DepthDayGraphDurationItem> DepthDayGraph { get; set; }

    public List<Cluster> Clusters { get; set; }
}