using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Core.DAL.RepositoryInterfaces;
using Core.Domain;
using Core.Domain.Attributes;
using Core.Models;

namespace Core.Services;

public abstract class BaseCatalogService<TDomain>
    where TDomain : NamedIdentity
{
    private readonly IRepository<TDomain> _repository;

    protected BaseCatalogService(IRepository<TDomain> catalogRepository)
    {
        _repository = catalogRepository;
    }

    public async Task<TDomain> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<PagedList<TDomain>> GetListAsync(PagingContext pagingContext, CancellationToken cancellationToken)
    {
        var factPageNumber = pagingContext.CurrentPage;
        var selection = await _repository.GetAllAsync(cancellationToken);

        List<Func<TDomain, bool>> predicates = new();

        if (!string.IsNullOrWhiteSpace(pagingContext.Filter))
            foreach (var propertyInfo in typeof(TDomain).GetProperties().Where(
                         _ => Attribute.IsDefined(_, typeof(SearchableAttribute))))
                predicates.Add(_ => FilterByProperty(_, propertyInfo, pagingContext.Filter));
        if (predicates.Any()) selection = selection.Where(_ => predicates.Any(p => p(_)));

        if (!string.IsNullOrEmpty(pagingContext.SortBy))
        {
            var propertyInfo = typeof(TDomain)
                .GetProperties()
                .FirstOrDefault(_ => _
                    .GetCustomAttribute<SortableAttribute>()
                    ?.Key
                    .Equals(pagingContext.SortBy, StringComparison.InvariantCultureIgnoreCase) ?? false);
            if (propertyInfo != null)
                selection = pagingContext.SortDesc
                    ? selection.OrderByDescending(_ => propertyInfo.GetValue(_, null))
                    : selection.OrderBy(_ => propertyInfo.GetValue(_, null));
        }

        var projectsList = selection.ToList();
        var totalProjects = projectsList.Count;
        if (totalProjects <= pagingContext.PerPage)
            return new PagedList<TDomain>
            {
                TotalRows = totalProjects,
                Items = projectsList,
                Page = 1,
                PerPage = pagingContext.PerPage
            };
        var totalPageAmount = totalProjects % pagingContext.PerPage + 1;
        if (factPageNumber > totalPageAmount) factPageNumber = totalPageAmount;
        var projectsInPage = projectsList.Skip((pagingContext.CurrentPage - 1) * pagingContext.PerPage)
            .Take(pagingContext.PerPage).ToList();

        return new PagedList<TDomain>
        {
            Items = projectsInPage,
            Page = factPageNumber,
            PerPage = pagingContext.PerPage,
            TotalRows = totalProjects
        };
    }

    private static bool FilterByProperty(TDomain domainModel, PropertyInfo propertyInfo, string filter)
    {
        return propertyInfo.GetValue(domainModel)!.ToString()!
            .Contains(filter, StringComparison.InvariantCultureIgnoreCase);
    }
}