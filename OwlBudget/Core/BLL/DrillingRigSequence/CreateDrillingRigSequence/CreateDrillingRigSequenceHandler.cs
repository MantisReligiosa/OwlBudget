using System.Threading;
using System.Threading.Tasks;
using Core.DAL.RepositoryInterfaces;
using MediatR;

namespace Core.BLL.DrillingRigSequence.CreateDrillingRigSequence;

public class
    CreateDrillingRigSequenceHandler : IRequestHandler<CreateDrillingRigSequenceRequest, Domain.DrillingRigSequence>
{
    private readonly IDrillingRigSequenceRepository _drillingRigSequenceRepository;

    public CreateDrillingRigSequenceHandler(IDrillingRigSequenceRepository drillingRigSequenceRepository)
    {
        _drillingRigSequenceRepository = drillingRigSequenceRepository;
    }

    public async Task<Domain.DrillingRigSequence> Handle(CreateDrillingRigSequenceRequest request,
        CancellationToken cancellationToken)
    {
        return await _drillingRigSequenceRepository.CreateAsync(new Domain.DrillingRigSequence
            {
                DrillingRigId = request.DrillingRigId,
                Name = "Последовательность",
                ScenarioId = request.ScenarioId
            },
            cancellationToken);
    }
}