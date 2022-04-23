using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.DAL.Entities;

[Table("Lots")]
public class LotEntity : NamedEntity
{
    [Column("ProjectId")] public Guid ProjectId { get; set; }

    public ProjectEntity Project { get; set; }

    public ICollection<ClusterEntity> Clusters { get; set; }
}