using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Core.DAL.RepositoryInterfaces;

public interface IDatabaseContext
{
    IQueryable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class;

    IQueryable<TEntity> Get<TEntity>() where TEntity : class;

    IQueryable<TEntity> GetWith<TEntity>(Expression<Func<TEntity, bool>> expression,
        params Expression<Func<TEntity, object>>[] includes) where TEntity : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

    void SetDeletedState<TEntity>(TEntity entity) where TEntity : class;

    void SetModifiedState<TEntity>(TEntity entity) where TEntity : class;

    void RemoveEntity<TEntity>(TEntity entity) where TEntity : class;

    Task<TEntity> AddEntityAsync<TEntity>(TEntity entities, CancellationToken cancellationToken) where TEntity : class;

    Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        where TEntity : class;

    Task<int> CountAsync<TEntity>(CancellationToken cancellationToken) where TEntity : class;

    Task<TEntity> FindByIdAsync<TEntity>(object id, CancellationToken cancellationToken) where TEntity : class;

    Task<TEntity> FirstAsync<TEntity>(CancellationToken cancellationToken) where TEntity : class;

    Task<TEntity> SingleAsync<TEntity>(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
        where TEntity : class;

    Task<TEntity> SingleOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken) where TEntity : class;

    int SaveChanges();
}