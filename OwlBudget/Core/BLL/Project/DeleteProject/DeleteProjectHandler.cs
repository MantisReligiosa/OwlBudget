using System.Threading;
using System.Threading.Tasks;
using Core.DAL.RepositoryInterfaces;
using MediatR;

namespace Core.BLL.Project.DeleteProject;

public class DeleteProjectHandler : AsyncRequestHandler<DeleteProjectRequest>
{
    private readonly IProjectRepository _projectRepository;

    public DeleteProjectHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    protected override async Task Handle(DeleteProjectRequest request, CancellationToken cancellationToken)
    {
        await _projectRepository.DeleteByIdAsync(request.ProjectId, cancellationToken);
    }
}