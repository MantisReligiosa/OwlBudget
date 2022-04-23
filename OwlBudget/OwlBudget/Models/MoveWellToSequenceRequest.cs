using System;

namespace OwlBudget.Models;

public class MoveWellToSequenceRequest
{
    public Guid CurrentSequenceId { get; set; }
    public Guid NewSequenceId { get; set; }
    public Guid WellId { get; set; }
    public int Order { get; set; }
}