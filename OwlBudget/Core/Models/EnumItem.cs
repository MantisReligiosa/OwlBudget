using System;

namespace Core.Models;

public class EnumItem<T>
    where T : Enum
{
    public T Value { get; set; }

    public string Description { get; set; }
}