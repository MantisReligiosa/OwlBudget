using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.DAL.Entities;

[Table("WellTypes")]
public class WellTypeEntity : NamedEntity
{
    public ICollection<DepthDayGraphDurationItemEntity> DepthDayGraph { get; set; }
    public ICollection<ClusterEntity> Clusters { get; set; }
}