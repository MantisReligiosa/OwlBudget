using System;
using System.Linq.Expressions;
using Core.DAL.Entities;

namespace Core.DAL.Specifications;

public static class EntitySpecification
{
    public static Expression<Func<Entity, bool>> ById(Guid id)
    {
        return _ => _.Id.Equals(id);
    }
}