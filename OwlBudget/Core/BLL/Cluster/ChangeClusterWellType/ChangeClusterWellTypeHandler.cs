using System.Threading;
using System.Threading.Tasks;
using Core.DAL.RepositoryInterfaces;
using Core.Models.OperationResults;
using MediatR;

namespace Core.BLL.Cluster.ChangeClusterWellType;

public class ChangeClusterWellTypeHandler : IRequestHandler<ChangeClusterWellTypeRequest, OperationResult>
{
    private readonly IClusterRepository _clusterRepository;

    public ChangeClusterWellTypeHandler(IClusterRepository clusterRepository)
    {
        _clusterRepository = clusterRepository;
    }

    public async Task<OperationResult> Handle(ChangeClusterWellTypeRequest request,
        CancellationToken cancellationToken)
    {
        var cluster = await _clusterRepository.GetByIdAsync(request.ClusterId, cancellationToken);

        if (cluster == null) return new ErrorOperationResult {ErrorMessage = "Куст не существует"};

        cluster.TypeId = request.WellTypeId;
        await _clusterRepository.UpdateAsync(cluster, cancellationToken);

        return new SuccessOperationResult();
    }
}