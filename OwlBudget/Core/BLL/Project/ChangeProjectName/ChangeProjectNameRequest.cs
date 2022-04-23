using System;
using Core.Models.OperationResults;
using MediatR;

namespace Core.BLL.Project.ChangeProjectName;

public record ChangeProjectNameRequest(string Name, Guid ProjectId) : IRequest<OperationResult>;