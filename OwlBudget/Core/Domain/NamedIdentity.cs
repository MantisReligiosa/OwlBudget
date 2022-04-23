using Core.Domain.Attributes;

namespace Core.Domain;

public abstract class NamedIdentity : Identity
{
    [Searchable] [Sortable("name")] public string Name { get; set; }
}