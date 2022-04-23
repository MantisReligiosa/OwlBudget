using System;
using System.Collections.Generic;

namespace OwlBudget.Models.ProjectObjectsModels;

public class Cluster
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid TypeId { get; set; }
    public List<Well> Wells { get; set; }
}