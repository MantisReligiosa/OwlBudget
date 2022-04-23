using System;
using Core.Models.OperationResults;
using MediatR;

namespace Core.BLL.Project.ChangeProjectDescription;

public record ChangeProjectDescriptionRequest(Guid ProjectId, string Description) : IRequest<OperationResult>;