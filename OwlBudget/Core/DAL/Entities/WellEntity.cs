using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.DAL.Entities;

[Table("Wells")]
public class WellEntity : NamedEntity
{
    [Column("ConstructionId")] public Guid ConstructionId { get; set; }

    [Column("ClusterId")] public Guid ClusterId { get; set; }

    public WellConstructionEntity Construction { get; set; }

    public ClusterEntity Cluster { get; set; }

    public ICollection<WellToDrillingRigSequenceEntity> WellsToDrillingRigSequences { get; set; }

    public ICollection<WellBuildingScheduleItemEntity> WellBuildingScheduleItems { get; set; }
}