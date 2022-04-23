using System;
using MediatR;

namespace Core.BLL.DrillingRigSequence.MoveWellToNewSequence;

public record MoveWellToNewSequenceRequest(Guid ScenarioId, Guid WellId) : IRequest;