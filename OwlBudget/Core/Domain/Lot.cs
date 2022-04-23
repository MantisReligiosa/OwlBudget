using System;
using System.Collections.Generic;

namespace Core.Domain;

public class Lot : NamedIdentity
{
    public Guid ProjectId { get; set; }

    public Project Project { get; set; }

    public List<Cluster> Clusters { get; set; }
}