using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Core.Domain;

public class IdentityComparer<T> : EqualityComparer<T>
    where T : Identity
{
    public override bool Equals(T x, T y)
    {
        return Equals(x.Id, y.Id);
    }

    public override int GetHashCode([DisallowNull] T obj)
    {
        return obj.Id.GetHashCode();
    }
}