using System;
using System.Collections.Generic;
using MediatR;

namespace Core.BLL.Lot.GetProjectLots;

public record GetProjectLotsRequest(Guid ProjectId) : IRequest<List<Domain.Lot>>;