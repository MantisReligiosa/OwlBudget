using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.DAL.RepositoryInterfaces;
using MediatR;

namespace Core.BLL.Cluster.CreateCluster;

public class CreateClusterHandler : AsyncRequestHandler<CreateClusterRequest>
{
    private readonly IClusterRepository _clusterRepository;
    private readonly IWellTypeRepository _wellTypeRepository;

    public CreateClusterHandler(IWellTypeRepository wellTypeRepository, IClusterRepository clusterRepository)
    {
        _wellTypeRepository = wellTypeRepository;
        _clusterRepository = clusterRepository;
    }

    protected override async Task Handle(CreateClusterRequest request, CancellationToken cancellationToken)
    {
        var wellTypeId = (await _wellTypeRepository.FirstAsync(cancellationToken)).Id;
        await _clusterRepository.CreateAsync(new Domain.Cluster
            {
                LotId = request.LotId,
                Name = "Новый куст",
                Wells = new List<Domain.Well>(),
                TypeId = wellTypeId
            },
            cancellationToken);
    }
}