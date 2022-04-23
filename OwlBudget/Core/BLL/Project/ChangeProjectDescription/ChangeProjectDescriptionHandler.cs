using System.Threading;
using System.Threading.Tasks;
using Core.DAL.RepositoryInterfaces;
using Core.Models.OperationResults;
using MediatR;

namespace Core.BLL.Project.ChangeProjectDescription;

public class ChangeProjectDescriptionHandler : IRequestHandler<ChangeProjectDescriptionRequest, OperationResult>
{
    private readonly IProjectRepository _projectRepository;

    public ChangeProjectDescriptionHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<OperationResult> Handle(ChangeProjectDescriptionRequest request,
        CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);

        if (project == null) return new ErrorOperationResult {ErrorMessage = "Проект не существует"};

        project.Description = request.Description;
        await _projectRepository.UpdateAsync(project, cancellationToken);

        return new SuccessOperationResult();
    }
}