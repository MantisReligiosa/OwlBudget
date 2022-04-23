using System;
using MediatR;

namespace Core.BLL.DrillingRigSequence.CreateDrillingRigSequence;

public record CreateDrillingRigSequenceRequest
    (Guid DrillingRigId, Guid ScenarioId) : IRequest<Domain.DrillingRigSequence>;