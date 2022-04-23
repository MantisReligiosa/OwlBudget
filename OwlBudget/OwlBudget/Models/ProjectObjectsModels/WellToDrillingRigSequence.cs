using System;

namespace OwlBudget.Models.ProjectObjectsModels;

public class WellToDrillingRigSequence
{
    public Guid DrillingRigSequenceId { get; set; }
    public Guid WellId { get; set; }
    public string WellName { get; set; }
    public int DrillingOrder { get; set; }
}