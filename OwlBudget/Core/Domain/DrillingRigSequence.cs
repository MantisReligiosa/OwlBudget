using System;
using System.Collections.Generic;

namespace Core.Domain;

public class DrillingRigSequence : NamedIdentity
{
    public Guid ScenarioId { get; set; }

    public Scenario Scenario { get; set; }

    public Guid DrillingRigId { get; set; }

    public DrillingRig DrillingRig { get; set; }

    public List<WellToDrillingRigSequence> WellsToDrillingRigSequence { get; set; }
}