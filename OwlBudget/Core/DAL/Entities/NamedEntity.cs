using System.ComponentModel.DataAnnotations.Schema;

namespace Core.DAL.Entities;

public abstract class NamedEntity : Entity
{
    [Column("Name")] public string Name { get; set; }
}