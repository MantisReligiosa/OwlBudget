using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Core.DAL.Entities;
using Core.DAL.RepositoryInterfaces;
using Core.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Core.DAL;

public class DatabaseContext : DbContext, IDatabaseContext
{
    private readonly IConfigService _configService;

    public DatabaseContext(IConfigService configService)
    {
        _configService = configService;
    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<WellTypeEntity> WellTypeEntities { get; set; }
    public DbSet<WellBuildingScheduleItemEntity> WellBuildingScheduleEntities { get; set; }

    public async Task<TEntity> AddEntityAsync<TEntity>(TEntity entities, CancellationToken cancellationToken)
        where TEntity : class
    {
        return (await Set<TEntity>().AddAsync(entities, cancellationToken)).Entity;
    }

    public async Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        where TEntity : class
    {
        await Set<TEntity>().AddRangeAsync(entities, cancellationToken);
    }

    public async Task<int> CountAsync<TEntity>(CancellationToken cancellationToken) where TEntity : class
    {
        return await Set<TEntity>().AsQueryable().CountAsync(cancellationToken);
    }

    public async Task<TEntity> FindByIdAsync<TEntity>(object id, CancellationToken cancellationToken)
        where TEntity : class
    {
        return await Set<TEntity>().FindAsync(new[] {id}, cancellationToken);
    }

    public async Task<TEntity> FirstAsync<TEntity>(CancellationToken cancellationToken) where TEntity : class
    {
        return await Set<TEntity>().FirstAsync(cancellationToken);
    }

    public IQueryable<TEntity> Get<TEntity>() where TEntity : class
    {
        return Set<TEntity>();
    }

    public IQueryable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class
    {
        return Set<TEntity>().Where(expression);
    }

    public IQueryable<TEntity> GetWith<TEntity>(Expression<Func<TEntity, bool>> expression,
        params Expression<Func<TEntity, object>>[] includes) where TEntity : class
    {
        var query = Set<TEntity>().AsQueryable();
        foreach (var include in includes) query = query.Include(include);
        return query.Where(expression);
    }

    public void RemoveEntity<TEntity>(TEntity entity) where TEntity : class
    {
        Set<TEntity>().Remove(entity);
    }

    public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
    {
        Set<TEntity>().RemoveRange(entities);
    }

    public void SetDeletedState<TEntity>(TEntity entity) where TEntity : class
    {
        Entry(entity).State = EntityState.Deleted;
    }

    public void SetModifiedState<TEntity>(TEntity entity) where TEntity : class
    {
        Entry(entity).State = EntityState.Modified;
    }

    public async Task<TEntity> SingleAsync<TEntity>(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken) where TEntity : class
    {
        return await Set<TEntity>().SingleOrDefaultAsync(expression, cancellationToken);
    }

    public async Task<TEntity> SingleOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken) where TEntity : class
    {
        return await Set<TEntity>().SingleOrDefaultAsync(expression, cancellationToken);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlite(_configService.ConnectionString);
    }
}