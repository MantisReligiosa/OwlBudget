using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Tools;

namespace Core.DAL.Entities;

[Table("Projects")]
public class ProjectEntity : NamedEntity
{
    [Column("Description")] public string Description { get; set; }

    [Column("OwnerId")] public Guid OwnerId { get; set; }

    [Column("ContractTypeId")] public int ContractTypeId { get; set; }

    [Column("Location")] public string Location { get; set; }

    [Column("RegionId")] public Guid RegionId { get; set; }

    [Column("CreatedTimestamp")] public long CreatedEpoch { get; set; }

    [Column("ModificatedTimestamp")] public long ModificationEpoch { get; set; }

    [NotMapped]
    public DateTime CreatedDt
    {
        get => DateTimeExtension.FromEpoch(CreatedEpoch);
        set => CreatedEpoch = value.ToEpoch();
    }

    [NotMapped]
    public DateTime ModifiedDt
    {
        get => DateTimeExtension.FromEpoch(ModificationEpoch);
        set => ModificationEpoch = value.ToEpoch();
    }

    public RegionEntity Region { get; set; }

    public UserEntity Owner { get; set; }

    public ICollection<LotEntity> Lots { get; set; }

    public ICollection<ScenarioEntity> Scenarios { get; set; }
}