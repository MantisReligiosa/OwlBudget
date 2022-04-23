using System.Collections.Generic;

namespace Core.Domain;

public class WellConstruction : NamedIdentity
{
    public List<Well> Wells { get; set; }
}