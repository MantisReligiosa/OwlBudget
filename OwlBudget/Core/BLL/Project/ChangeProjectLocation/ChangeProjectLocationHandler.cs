using System.Threading;
using System.Threading.Tasks;
using Core.DAL.RepositoryInterfaces;
using Core.Models.OperationResults;
using MediatR;

namespace Core.BLL.Project.ChangeProjectLocation;

public class ChangeProjectLocationHandler : IRequestHandler<ChangeProjectLocationRequest, OperationResult>
{
    private readonly IProjectRepository _projectRepository;

    public ChangeProjectLocationHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<OperationResult> Handle(ChangeProjectLocationRequest request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Location))
            return new ErrorOperationResult {ErrorMessage = "Обязательное поле"};

        var project = await _projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);

        if (project == null) return new ErrorOperationResult {ErrorMessage = "Проект не существует"};

        project.Location = request.Location;
        await _projectRepository.UpdateAsync(project, cancellationToken);

        return new SuccessOperationResult();
    }
}