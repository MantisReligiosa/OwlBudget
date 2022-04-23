using System.Threading;
using System.Threading.Tasks;
using Core.DAL.RepositoryInterfaces;
using Core.Models.OperationResults;
using MediatR;

namespace Core.BLL.DrillingRigSequence.ChangeDrillingRigSequenceName;

public class
    ChangeDrillingRigSequenceNameHandler : IRequestHandler<ChangeDrillingRigSequenceNameRequest, OperationResult>
{
    private readonly IDrillingRigSequenceRepository _drillingRigSequenceRepository;

    public ChangeDrillingRigSequenceNameHandler(IDrillingRigSequenceRepository drillingRigSequenceRepository)
    {
        _drillingRigSequenceRepository = drillingRigSequenceRepository;
    }

    public async Task<OperationResult> Handle(ChangeDrillingRigSequenceNameRequest request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return new ErrorOperationResult {ErrorMessage = "Поле не может быть пустым"};

        var sequence =
            await _drillingRigSequenceRepository.GetByIdAsync(request.DrillingRigSequenceId, cancellationToken);

        if (sequence == null)
            return new ErrorOperationResult {ErrorMessage = "Последовательность не существует"};

        sequence.Name = request.Name;
        await _drillingRigSequenceRepository.UpdateAsync(sequence, cancellationToken);

        return new SuccessOperationResult();
    }
}