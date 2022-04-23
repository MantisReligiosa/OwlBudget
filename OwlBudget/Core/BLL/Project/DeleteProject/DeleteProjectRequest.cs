using System;
using MediatR;

namespace Core.BLL.Project.DeleteProject;

public record DeleteProjectRequest(Guid ProjectId) : IRequest;