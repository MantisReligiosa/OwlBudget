using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.DAL.Entities;

[Table("Clusters")]
public class ClusterEntity : NamedEntity
{
    [Column("LotId")] public Guid LotId { get; set; }

    [Column("WellTypeId")] public Guid WellTypeId { get; set; }

    public LotEntity Lot { get; set; }

    public WellTypeEntity WellType { get; set; }

    public ICollection<WellEntity> Wells { get; set; }
}