using System;
using Core.Domain;
using Core.Models;
using MediatR;

namespace Core.BLL.DataCatalog.GetList;

public record GetListRequest<T>
    (Guid UserId, string Filter, string SortBy, int CurrentPage, int PerPage, bool SortDesc) : IRequest<PagedList<T>>
    where T : NamedIdentity;