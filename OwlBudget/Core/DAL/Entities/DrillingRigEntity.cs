using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.DAL.Entities;

[Table("DrillingRigs")]
public class DrillingRigEntity : NamedEntity
{
    /// <summary>
    /// Длительность ПЗР в днях
    /// </summary>
    public int PreparationWorkDurationDays { get; set; }
    
    /// <summary>
    /// Длительность ВМР (монтаж) в днях
    /// </summary>
    public int InstallationWorkDurationDays { get; set; }
    
    /// <summary>
    /// Длительность ВМР (демонтаж) в днях
    /// </summary>
    public int UninstallationWorkDurationDays { get; set; }
    public ICollection<DrillingRigSequenceEntity> DrillingRigSequences { get; set; }
}