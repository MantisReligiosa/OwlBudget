using System.Threading;
using System.Threading.Tasks;
using Core.DAL.RepositoryInterfaces;
using Core.Models.OperationResults;
using MediatR;

namespace Core.BLL.Lot.ChangeLotName;

public class ChangeLotNameHandler : IRequestHandler<ChangeLotNameRequest, OperationResult>
{
    private readonly ILotRepository _lotRepository;

    public ChangeLotNameHandler(ILotRepository lotRepository)
    {
        _lotRepository = lotRepository;
    }

    public async Task<OperationResult> Handle(ChangeLotNameRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return new ErrorOperationResult {ErrorMessage = "Поле не может быть пустым"};

        var lot = await _lotRepository.GetByIdAsync(request.LotId, cancellationToken);

        if (lot == null) return new ErrorOperationResult {ErrorMessage = "Лот не существует"};

        lot.Name = request.Name;
        await _lotRepository.UpdateAsync(lot, cancellationToken);

        return new SuccessOperationResult();
    }
}