using System;
using System.Collections.Generic;

namespace Core.Domain;

public class Well : NamedIdentity
{
    public Guid ConstructionId { get; set; }

    public WellConstruction Construction { get; set; }

    public Guid ClusterId { get; set; }

    public Cluster Cluster { get; set; }

    public List<WellToDrillingRigSequence> WellsToDrillingRigSequences { get; set; }

    public List<WellBuildingScheduleItem> WellBuildingScheduleItems { get; set; }
}