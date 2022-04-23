using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.DAL.Entities;

[Table("Users")]
public class UserEntity : Entity
{
    [Column("Login")] public string Login { get; set; }

    [Column("PasswordHash")] public string PasswordHash { get; set; }

    [Column("Role")] public int RoleId { get; set; }

    public ICollection<ProjectEntity> Projects { get; set; }
}