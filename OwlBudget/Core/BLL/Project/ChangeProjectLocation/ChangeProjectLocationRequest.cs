using System;
using Core.Models.OperationResults;
using MediatR;

namespace Core.BLL.Project.ChangeProjectLocation;

public record ChangeProjectLocationRequest(string Location, Guid ProjectId) : IRequest<OperationResult>;