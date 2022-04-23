using System.Threading;
using System.Threading.Tasks;
using Core.DAL.RepositoryInterfaces;
using Core.Models.OperationResults;
using MediatR;

namespace Core.BLL.Project.ChangeProjectName;

public class ChangeProjectNameHandler : IRequestHandler<ChangeProjectNameRequest, OperationResult>
{
    private readonly IProjectRepository _projectRepository;

    public ChangeProjectNameHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<OperationResult> Handle(ChangeProjectNameRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return new ErrorOperationResult {ErrorMessage = "Обязательное поле"};
        if (await _projectRepository.IsNameInUseAsync(request.Name, request.ProjectId, cancellationToken))
            return new ErrorOperationResult {ErrorMessage = "Имя уже используется другим проектом"};

        var project = await _projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);

        if (project == null) return new ErrorOperationResult {ErrorMessage = "Проект не существует"};

        project.Name = request.Name;
        await _projectRepository.UpdateAsync(project, cancellationToken);

        return new SuccessOperationResult();
    }
}