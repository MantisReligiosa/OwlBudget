using System.ComponentModel;

namespace Core.Domain;

public enum ContractTypes
{
    /// <summary>
    ///     Под ключ
    /// </summary>
    [Description("Под ключ")] TurnKey = 1,

    /// <summary>
    ///     По суточной ставке
    /// </summary>
    [Description("По суточной ставке")] PerDiemRate = 2
}