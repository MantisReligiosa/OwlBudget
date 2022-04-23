using System;
using MediatR;

namespace Core.BLL.DrillingRigSequence.MoveWellToSequence;

public record MoveWellToSequenceRequest(Guid CurrentSequenceId, Guid NewSequenceId, Guid WellId, int Order) : IRequest;