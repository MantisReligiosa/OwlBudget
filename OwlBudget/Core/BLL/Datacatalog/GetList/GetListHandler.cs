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
using MediatR;

namespace Core.BLL.DataCatalog.GetList;

public class GetListHandler<T> : IRequestHandler<GetListRequest<T>, PagedList<T>>
    where T : NamedIdentity
{
    private readonly IRepository<T> _repository;

    public GetListHandler(IRepository<T> repository)
    {
        _repository = repository;
    }

    public async Task<PagedList<T>> Handle(GetListRequest<T> request, CancellationToken cancellationToken)
    {
        var factPageNumber = request.CurrentPage;
        var selection = await _repository.GetAllAsync(cancellationToken);

        List<Func<T, bool>> predicates = new();

        if (!string.IsNullOrWhiteSpace(request.Filter))
            foreach (var propertyInfo in typeof(T).GetProperties().Where(
                         _ => Attribute.IsDefined(_, typeof(SearchableAttribute))))
                predicates.Add(_ => FilterByProperty(_, propertyInfo, request.Filter));
        if (predicates.Any()) selection = selection.Where(_ => predicates.Any(p => p(_)));

        if (!string.IsNullOrEmpty(request.SortBy))
        {
            var propertyInfo = typeof(T)
                .GetProperties()
                .FirstOrDefault(_ => _
                    .GetCustomAttribute<SortableAttribute>()
                    ?.Key
                    .Equals(request.SortBy, StringComparison.InvariantCultureIgnoreCase) ?? false);
            if (propertyInfo != null)
                selection = request.SortDesc
                    ? selection.OrderByDescending(_ => propertyInfo.GetValue(_, null))
                    : selection.OrderBy(_ => propertyInfo.GetValue(_, null));
        }

        var projectsList = selection.ToList();
        var totalProjects = projectsList.Count;
        if (totalProjects <= request.PerPage)
            return new PagedList<T>
            {
                TotalRows = totalProjects,
                Items = projectsList,
                Page = 1,
                PerPage = request.PerPage
            };
        var totalPageAmount = totalProjects % request.PerPage + 1;
        if (factPageNumber > totalPageAmount) factPageNumber = totalPageAmount;
        var projectsInPage = projectsList.Skip((request.CurrentPage - 1) * request.PerPage).Take(request.PerPage)
            .ToList();

        return new PagedList<T>
        {
            Items = projectsInPage,
            Page = factPageNumber,
            PerPage = request.PerPage,
            TotalRows = totalProjects
        };
    }

    private static bool FilterByProperty(T domainModel, PropertyInfo propertyInfo, string filter)
    {
        return propertyInfo.GetValue(domainModel)!.ToString()!
            .Contains(filter, StringComparison.InvariantCultureIgnoreCase);
    }
}