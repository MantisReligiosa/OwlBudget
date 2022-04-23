using System.Threading;
using System.Threading.Tasks;
using Core.DAL.RepositoryInterfaces;
using MediatR;

namespace Core.BLL.Lot.DeleteLot;

public class DeleteLotHandler : AsyncRequestHandler<DeleteLotRequest>
{
    private readonly ILotRepository _lotRepository;

    public DeleteLotHandler(ILotRepository lotRepository)
    {
        _lotRepository = lotRepository;
    }

    protected override async Task Handle(DeleteLotRequest request, CancellationToken cancellationToken)
    {
        await _lotRepository.DeleteByIdAsync(request.LotId, cancellationToken);
    }
}