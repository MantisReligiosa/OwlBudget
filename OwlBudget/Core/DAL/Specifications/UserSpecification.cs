using System;
using System.Linq.Expressions;
using Core.DAL.Entities;

namespace Core.DAL.Specifications;

public static class UserSpecification
{
    public static Expression<Func<UserEntity, bool>> ByName(string name)
    {
        return u => u.Login.Equals(name);
    }
}