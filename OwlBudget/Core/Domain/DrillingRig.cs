using System.Collections.Generic;

namespace Core.Domain;

/// <summary>
/// Буровая установка
/// </summary>
public class DrillingRig : NamedIdentity
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

    public List<DrillingRigSequence> DrillingRigSequences { get; set; }
}