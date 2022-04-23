using System;
using MediatR;

namespace Core.BLL.Cluster.CreateCluster;

public record CreateClusterRequest(Guid LotId) : IRequest;