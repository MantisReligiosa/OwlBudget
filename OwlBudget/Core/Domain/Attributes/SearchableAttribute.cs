using System;

namespace Core.Domain.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class SearchableAttribute : Attribute
{
}