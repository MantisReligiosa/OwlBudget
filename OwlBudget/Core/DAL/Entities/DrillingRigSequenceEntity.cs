using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.DAL.Entities;

[Table("DrillingRigSequences")]
public class DrillingRigSequenceEntity : NamedEntity
{
    [Column("ScenarioId")] public Guid ScenarioId { get; set; }

    [Column("DrillingRigId")] public Guid DrillingRigId { get; set; }

    public ScenarioEntity Scenario { get; set; }

    public DrillingRigEntity DrillingRig { get; set; }

    public ICollection<WellToDrillingRigSequenceEntity> WellsToDrillingRigSequence { get; set; }
}