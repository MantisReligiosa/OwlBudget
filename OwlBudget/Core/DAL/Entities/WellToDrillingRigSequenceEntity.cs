using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.DAL.Entities;

[Table("WellsToDrillingRigSequences")]
public class WellToDrillingRigSequenceEntity : Entity
{
    [Column("WellId")] public Guid WellId { get; set; }

    [Column("DrillingRigSequenceId")] public Guid DrillingRigSequenceId { get; set; }

    [Column("DrillingOrder")] public int DrillingOrder { get; set; }

    public WellEntity Well { get; set; }

    public DrillingRigSequenceEntity DrillingRigSequence { get; set; }
}