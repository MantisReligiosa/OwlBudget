using System;

namespace Core.Domain;

public class Scenario : NamedIdentity
{
    public Guid ProjectId { get; set; }

    public virtual Project Project { get; set; }

    public int BudgetType { get; set; }
}