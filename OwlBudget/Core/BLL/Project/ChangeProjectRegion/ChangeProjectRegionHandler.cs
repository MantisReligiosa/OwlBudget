using System.Threading;
using System.Threading.Tasks;
using Core.DAL.RepositoryInterfaces;
using Core.Models.OperationResults;
using MediatR;

namespace Core.BLL.Project.ChangeProjectRegion;

public class ChangeProjectRegionHandler : IRequestHandler<ChangeProjectRegionRequest, OperationResult>
{
    private readonly IProjectRepository _projectRepository;

    public ChangeProjectRegionHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<OperationResult> Handle(ChangeProjectRegionRequest request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);

        if (project == null) return new ErrorOperationResult {ErrorMessage = "Проект не существует"};

        project.RegionId = request.RegionId;
        await _projectRepository.UpdateAsync(project, cancellationToken);

        return new SuccessOperationResult();
    }
}