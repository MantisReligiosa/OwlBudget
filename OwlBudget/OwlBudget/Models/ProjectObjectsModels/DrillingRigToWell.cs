using System;

namespace OwlBudget.Models.ProjectObjectsModels;

public class DrillingRigToWell
{
    public Guid Id { get; set; }
    public Guid DrillingRigId { get; set; }
    public string DrillingRigName { get; set; }
    public Guid WellId { get; set; }
    public Guid ScenarioId { get; set; }
}