using System.Threading;
using System.Threading.Tasks;
using Core.DAL.RepositoryInterfaces;
using Core.Models.OperationResults;
using MediatR;

namespace Core.BLL.Well.ChangeWellConstruction;

public class ChangeWellConstructionHandler : IRequestHandler<ChangeWellConstructionRequest, OperationResult>
{
    private readonly IWellRepository _wellRepository;

    public ChangeWellConstructionHandler(IWellRepository wellRepository)
    {
        _wellRepository = wellRepository;
    }

    public async Task<OperationResult> Handle(ChangeWellConstructionRequest request,
        CancellationToken cancellationToken)
    {
        var well = await _wellRepository.GetByIdAsync(request.WellId, cancellationToken);

        if (well == null) return new ErrorOperationResult {ErrorMessage = "Скважина не существует"};

        well.ConstructionId = request.ConstructionId;
        await _wellRepository.UpdateAsync(well, cancellationToken);

        return new SuccessOperationResult();
    }
}