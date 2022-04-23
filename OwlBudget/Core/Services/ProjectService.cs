using Core.DAL.RepositoryInterfaces;
using Core.Domain;
using Core.ServiceInterfaces;

namespace Core.Services;

public class ProjectService : BaseCatalogService<Project>, IProjectService
{
    public ProjectService(IProjectRepository projectRepository)
        : base(projectRepository)
    {
    }
}