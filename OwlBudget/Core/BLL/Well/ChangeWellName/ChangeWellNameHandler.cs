using System.Threading;
using System.Threading.Tasks;
using Core.DAL.RepositoryInterfaces;
using Core.Models.OperationResults;
using MediatR;

namespace Core.BLL.Well.ChangeWellName;

public class ChangeWellNameHandler : IRequestHandler<ChangeWellNameRequest, OperationResult>
{
    private readonly IWellRepository _wellRepository;

    public ChangeWellNameHandler(IWellRepository wellRepository)
    {
        _wellRepository = wellRepository;
    }

    public async Task<OperationResult> Handle(ChangeWellNameRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return new ErrorOperationResult {ErrorMessage = "Поле не может быть пустым"};

        var well = await _wellRepository.GetByIdAsync(request.WellId, cancellationToken);

        if (well == null) return new ErrorOperationResult {ErrorMessage = "Скважина не существует"};

        well.Name = request.Name;
        await _wellRepository.UpdateAsync(well, cancellationToken);

        return new SuccessOperationResult();
    }
}