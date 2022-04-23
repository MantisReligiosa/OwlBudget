using System;
using Core.Domain;

namespace OwlBudget.Models;

public class ProjectParamsResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public AccessMode AccessMode { get; set; }
    public string Description { get; set; }
    public int ContractTypeId { get; set; }
    public string Location { get; set; }
    public Guid RegionId { get; set; }
}