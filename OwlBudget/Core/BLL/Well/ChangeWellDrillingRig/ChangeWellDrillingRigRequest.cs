using System;
using Core.Models.OperationResults;
using MediatR;

namespace Core.BLL.Well.ChangeWellDrillingRig;

public record ChangeWellDrillingRigRequest
    (Guid DrillingRigId, Guid ScenarioId, Guid WellId) : IRequest<OperationResult>;