using System;

namespace Core.Domain.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class SortableAttribute : Attribute
{
    public SortableAttribute(string key)
    {
        Key = key;
    }

    public string Key { get; }
}