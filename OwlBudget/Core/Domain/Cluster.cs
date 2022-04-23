using System;
using System.Collections.Generic;

namespace Core.Domain;

public class Cluster : NamedIdentity
{
    public Guid LotId { get; set; }

    public Lot Lot { get; set; }

    public Guid TypeId { get; set; }

    public WellType Type { get; set; }

    public List<Well> Wells { get; set; }
}