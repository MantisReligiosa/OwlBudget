using System;

namespace OwlBudget.Models;

public class MoveWellToNewSequenceRequest
{
    public Guid WellId { get; set; }
    public Guid ScenarioId { get; set; }
}