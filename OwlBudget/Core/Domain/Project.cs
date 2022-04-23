using System;
using System.Collections.Generic;
using Core.Domain.Attributes;

namespace Core.Domain;

public class Project : NamedIdentity
{
    public ContractTypes ContractType { get; set; }

    public string Location { get; set; }

    public Guid RegionId { get; set; }

    public Region Region { get; set; }

    [Searchable] public string Description { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; }

    public List<Lot> Lots { get; set; }

    public List<Scenario> Scenarios { get; set; }

    public DateTime CreatedDt { get; set; }

    public DateTime ModifiedDt { get; set; }
}