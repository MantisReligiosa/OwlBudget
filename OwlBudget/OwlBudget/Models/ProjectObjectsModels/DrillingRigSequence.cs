using System;
using System.Collections.Generic;

namespace OwlBudget.Models.ProjectObjectsModels;

public class DrillingRigSequence
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid ScenarioId { get; set; }
    public Guid DrillingRigId { get; set; }
    public string DrillingRigName { get; set; }
    public List<WellToDrillingRigSequence> WellsToDrillingRigSequence { get; set; }
}