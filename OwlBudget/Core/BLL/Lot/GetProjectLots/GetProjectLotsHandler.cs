using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.DAL.RepositoryInterfaces;
using MediatR;

namespace Core.BLL.Lot.GetProjectLots;

public class GetProjectLotsHandler : IRequestHandler<GetProjectLotsRequest, List<Domain.Lot>>
{
    private readonly ILotRepository _lotRepository;

    public GetProjectLotsHandler(ILotRepository lotRepository)
    {
        _lotRepository = lotRepository;
    }

    public async Task<List<Domain.Lot>> Handle(GetProjectLotsRequest request, CancellationToken cancellationToken)
    {
        return await _lotRepository.GetProjectLotsAsync(request.ProjectId, cancellationToken);
    }
}