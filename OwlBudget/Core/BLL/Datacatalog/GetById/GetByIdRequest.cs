using System;
using Core.Domain;
using MediatR;

namespace Core.BLL.DataCatalog.GetById;

public record GetByIdRequest<T>(Guid Id) : IRequest<T>
    where T : NamedIdentity;