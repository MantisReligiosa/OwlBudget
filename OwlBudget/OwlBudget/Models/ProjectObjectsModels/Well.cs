using System;
using System.Collections.Generic;

namespace OwlBudget.Models.ProjectObjectsModels;

public class Well
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid ConstructionId { get; set; }
    public List<DrillingRigToWell> DrillingRigsToWell { get; set; }
}