using System;
using System.Collections.Generic;
using MediatR;

namespace Core.BLL.Well.GetWellsHeaders;

public record GetWellsHeadersRequest(Guid ProjectId) : IRequest<List<WellHeader>>;