using System.Collections.Generic;

namespace Core.Domain;

public class User : Identity
{
    public string Login { get; set; }

    public string PasswordHash { get; set; }

    public Role Role { get; set; }

    public ICollection<Project> Projects { get; set; }
}