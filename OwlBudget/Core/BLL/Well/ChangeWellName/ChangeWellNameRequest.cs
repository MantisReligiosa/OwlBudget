using System;
using Core.Models.OperationResults;
using MediatR;

namespace Core.BLL.Well.ChangeWellName;

public record ChangeWellNameRequest(string Name, Guid WellId) : IRequest<OperationResult>;