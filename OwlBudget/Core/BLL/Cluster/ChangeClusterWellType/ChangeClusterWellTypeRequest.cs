using System;
using Core.Models.OperationResults;
using MediatR;

namespace Core.BLL.Cluster.ChangeClusterWellType;

public record ChangeClusterWellTypeRequest(Guid WellTypeId, Guid ClusterId) : IRequest<OperationResult>;