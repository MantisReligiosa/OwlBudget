using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.DAL.RepositoryInterfaces;
using MediatR;

namespace Core.BLL.Lot.CreateLot;

public class CreateLotHandler : AsyncRequestHandler<CreateLotRequest>
{
    private readonly ILotRepository _lotRepository;

    public CreateLotHandler(ILotRepository lotRepository)
    {
        _lotRepository = lotRepository;
    }

    protected override async Task Handle(CreateLotRequest request, CancellationToken cancellationToken)
    {
        await _lotRepository.CreateAsync(new Domain.Lot
            {
                Clusters = new List<Domain.Cluster>(),
                Name = "Новый лот",
                ProjectId = request.ProjectId
            },
            cancellationToken);
    }
}