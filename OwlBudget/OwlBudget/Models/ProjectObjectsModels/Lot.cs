using System;
using System.Collections.Generic;

namespace OwlBudget.Models.ProjectObjectsModels;

public class Lot
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Cluster> Clusters { get; set; }
}