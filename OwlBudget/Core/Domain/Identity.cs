using System;

namespace Core.Domain;

public abstract class Identity
{
    public Identity()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
}