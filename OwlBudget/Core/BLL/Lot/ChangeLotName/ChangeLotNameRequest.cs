using System;
using Core.Models.OperationResults;
using MediatR;

namespace Core.BLL.Lot.ChangeLotName;

public record ChangeLotNameRequest(string Name, Guid LotId) : IRequest<OperationResult>;