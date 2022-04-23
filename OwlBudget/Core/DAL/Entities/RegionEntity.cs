using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.DAL.Entities;

[Table("Regions")]
public class RegionEntity : NamedEntity
{
    public ICollection<ProjectEntity> Projects { get; set; }
}