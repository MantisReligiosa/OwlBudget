using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain;

namespace Core.DAL.RepositoryInterfaces;

public interface IRepository<TModel> where TModel : Identity
{
    Task<int> CountAsync(CancellationToken cancellationToken);
    Task<TModel> CreateAsync(TModel item, CancellationToken cancellationToken);
    Task DeleteByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteManyAsync(IEnumerable<Identity> identities, CancellationToken cancellationToken);
    Task<TModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<TModel>> GetAllAsync(CancellationToken cancellationToken);
    Task UpdateAsync(TModel item, CancellationToken cancellationToken);
    void Update(TModel item);
    Task<TModel> FirstAsync(CancellationToken cancellationToken);
    Task CreateManyAsync(IEnumerable<TModel> items, CancellationToken cancellationToken);
}