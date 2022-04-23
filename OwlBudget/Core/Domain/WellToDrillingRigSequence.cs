using System;

namespace Core.Domain;

public class WellToDrillingRigSequence : Identity
{
    public Guid WellId { get; set; }

    public Well Well { get; set; }

    public Guid DrillingRigSequenceId { get; set; }

    public DrillingRigSequence DrillingRigSequence { get; set; }

    public int DrillingOrder { get; set; }
}