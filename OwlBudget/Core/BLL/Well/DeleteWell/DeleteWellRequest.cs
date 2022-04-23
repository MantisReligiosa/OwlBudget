using System;
using MediatR;

namespace Core.BLL.Well.DeleteWell;

public record DeleteWellRequest(Guid WellId) : IRequest;