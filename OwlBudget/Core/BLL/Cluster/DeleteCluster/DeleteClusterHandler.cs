using System.Threading;
using System.Threading.Tasks;
using Core.DAL.RepositoryInterfaces;
using MediatR;

namespace Core.BLL.Cluster.DeleteCluster;

public class DeleteClusterHandler : AsyncRequestHandler<DeleteClusterRequest>
{
    private readonly IClusterRepository _clusterRepository;

    public DeleteClusterHandler(IClusterRepository clusterRepository)
    {
        _clusterRepository = clusterRepository;
    }

    protected override async Task Handle(DeleteClusterRequest request, CancellationToken cancellationToken)
    {
        await _clusterRepository.DeleteByIdAsync(request.ClusterId, cancellationToken);
    }
}