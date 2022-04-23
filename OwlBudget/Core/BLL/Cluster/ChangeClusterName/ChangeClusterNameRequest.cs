using System;
using Core.Models.OperationResults;
using MediatR;

namespace Core.BLL.Cluster.ChangeClusterName;

public record ChangeClusterNameRequest(string Name, Guid ClusterId) : IRequest<OperationResult>;