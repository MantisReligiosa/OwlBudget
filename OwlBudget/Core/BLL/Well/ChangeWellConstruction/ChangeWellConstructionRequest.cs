using System;
using Core.Models.OperationResults;
using MediatR;

namespace Core.BLL.Well.ChangeWellConstruction;

public record ChangeWellConstructionRequest(Guid ConstructionId, Guid WellId) : IRequest<OperationResult>;