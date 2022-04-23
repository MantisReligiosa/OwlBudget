using System;
using MediatR;

namespace Core.BLL.Project.CreateProject;

public record CreateProjectRequest(Guid UserId) : IRequest<Domain.Project>;