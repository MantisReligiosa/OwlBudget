using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.DAL.Entities;

[Table("WellConstructions")]
public class WellConstructionEntity : NamedEntity
{
    public ICollection<WellEntity> Wells { get; set; }
}