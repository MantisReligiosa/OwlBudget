using System.Collections.Generic;

namespace Core.Domain;

public class Region : NamedIdentity
{
    public List<Project> Projects { get; set; }
}