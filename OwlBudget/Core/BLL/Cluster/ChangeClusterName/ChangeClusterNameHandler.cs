using System.Threading;
using System.Threading.Tasks;
using Core.DAL.RepositoryInterfaces;
using Core.Models.OperationResults;
using MediatR;

namespace Core.BLL.Cluster.ChangeClusterName;

public class ChangeClusterNameHandler : IRequestHandler<ChangeClusterNameRequest, OperationResult>
{
    private readonly IClusterRepository _clusterRepository;

    public ChangeClusterNameHandler(IClusterRepository clusterRepository)
    {
        _clusterRepository = clusterRepository;
    }

    public async Task<OperationResult> Handle(ChangeClusterNameRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return new ErrorOperationResult {ErrorMessage = "Обязательное поле"};

        var cluster = await _clusterRepository.GetByIdAsync(request.ClusterId, cancellationToken);

        if (cluster == null) return new ErrorOperationResult {ErrorMessage = "Куст не существует"};

        cluster.Name = request.Name;
        await _clusterRepository.UpdateAsync(cluster, cancellationToken);

        return new SuccessOperationResult();
    }
}