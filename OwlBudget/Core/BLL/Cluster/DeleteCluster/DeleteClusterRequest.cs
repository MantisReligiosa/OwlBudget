using System;
using MediatR;

namespace Core.BLL.Cluster.DeleteCluster;

public record DeleteClusterRequest(Guid ClusterId) : IRequest;