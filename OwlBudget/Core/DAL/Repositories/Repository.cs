using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Entities;
using Core.DAL.RepositoryInterfaces;
using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Core.DAL.Repositories;

public abstract class Repository<TModel, TEntity> : IRepository<TModel>
    where TModel : Identity
    where TEntity : Entity
{
    internal readonly IDatabaseContext _context;
    internal IMapper _mapper;

    protected Repository(IDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken)
    {
        return await _context.CountAsync<TEntity>(cancellationToken);
    }

    public virtual async Task<TModel> CreateAsync(TModel item, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<TModel, TEntity>(item);
        var savedEntity = await _context.AddEntityAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<TEntity, TModel>(savedEntity);
    }

    public virtual async Task CreateManyAsync(IEnumerable<TModel> items, CancellationToken cancellationToken)
    {
        var entities = _mapper.Map<IEnumerable<TEntity>>(items);
        await _context.AddRangeAsync(entities, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.FindByIdAsync<TEntity>(id, cancellationToken);
        if (entity != null)
        {
            _context.RemoveEntity(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public virtual async Task DeleteByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
    {
        if (ids == null || !ids.Any()) return;

        foreach (var id in ids)
        {
            var entity = await _context.FindByIdAsync<TEntity>(id, cancellationToken);
            if (entity != null) _context.RemoveEntity(entity);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteManyAsync(IEnumerable<Identity> identities, CancellationToken cancellationToken)
    {
        await DeleteByIdsAsync(identities.Select(i => i.Id), cancellationToken);
    }

    public virtual async Task<TModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.FindByIdAsync<TEntity>(id, cancellationToken);
        return _mapper.Map<TModel>(entity);
    }

    public virtual async Task<IEnumerable<TModel>> GetAllAsync(CancellationToken cancellationToken)
    {
        var entities = await _context.Get<TEntity>().ToListAsync(cancellationToken);
        var result = _mapper.Map<IEnumerable<TModel>>(entities);
        return result;
    }

    public virtual async Task UpdateAsync(TModel item, CancellationToken cancellationToken)
    {
        var entity = await _context.FindByIdAsync<TEntity>(item.Id, cancellationToken);
        _mapper.Map(item, entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<TModel> FirstAsync(CancellationToken cancellationToken)
    {
        var entity = await _context.FirstAsync<TEntity>(cancellationToken);
        return _mapper.Map<TModel>(entity);
    }

    public void Update(TModel item)
    {
        var entity = _context.Get<TEntity>(_ => _.Id == item.Id).First();
        _mapper.Map(item, entity);
        _context.SaveChanges();
    }

    protected async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken)
    {
        return await _context.Get(expression).ToListAsync(cancellationToken);
    }
}