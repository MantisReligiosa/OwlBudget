using System;
using MediatR;

namespace Core.BLL.Well.CreateWell;

public record CreateWellRequest(Guid ClusterId) : IRequest;