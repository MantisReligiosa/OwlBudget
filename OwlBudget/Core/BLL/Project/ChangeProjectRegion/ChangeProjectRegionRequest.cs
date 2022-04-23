using System;
using Core.Models.OperationResults;
using MediatR;

namespace Core.BLL.Project.ChangeProjectRegion;

public record ChangeProjectRegionRequest(Guid ProjectId, Guid RegionId) : IRequest<OperationResult>;