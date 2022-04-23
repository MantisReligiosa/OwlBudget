using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using Core.Models;

namespace Core.ServiceInterfaces;

public interface ICatalogService<TNamedIdentity>
    where TNamedIdentity : NamedIdentity
{
    Task<TNamedIdentity> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<PagedList<TNamedIdentity>> GetListAsync(PagingContext pagingContext, CancellationToken cancellationToken);
}