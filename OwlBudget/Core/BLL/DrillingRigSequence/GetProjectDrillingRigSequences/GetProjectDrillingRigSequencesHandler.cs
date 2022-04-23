using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.DAL.RepositoryInterfaces;
using MediatR;

namespace Core.BLL.DrillingRigSequence.GetProjectDrillingRigSequences;

public class
    GetProjectDrillingRigSequencesHandler : IRequestHandler<GetProjectDrillingRigSequencesRequest,
        List<Domain.DrillingRigSequence>>
{
    private readonly IDrillingRigSequenceRepository _drillingRigSequenceRepository;

    public GetProjectDrillingRigSequencesHandler(IDrillingRigSequenceRepository drillingRigSequenceRepository)
    {
        _drillingRigSequenceRepository = drillingRigSequenceRepository;
    }

    public async Task<List<Domain.DrillingRigSequence>> Handle(GetProjectDrillingRigSequencesRequest request,
        CancellationToken cancellationToken)
    {
        return await _drillingRigSequenceRepository.GetProjectSequencesAsync(request.ProjectId, cancellationToken);
    }
}