using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.DAL.RepositoryInterfaces;
using MediatR;

namespace Core.BLL.Well.GetWellsHeaders;

public class GetWellsHeadersHandler : IRequestHandler<GetWellsHeadersRequest, List<WellHeader>>
{
    private readonly IWellRepository _wellRepository;

    public GetWellsHeadersHandler(IWellRepository wellRepository)
    {
        _wellRepository = wellRepository;
    }

    public async Task<List<WellHeader>> Handle(GetWellsHeadersRequest request, CancellationToken cancellationToken)
    {
        return (await _wellRepository.GetForProject(request.ProjectId, cancellationToken)).Select(well =>
            new WellHeader(well.Id, well.Name)).ToList();
    }
}