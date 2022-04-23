using System;
using MediatR;

namespace Core.BLL.DrillingRigSequence.ExcludeWellFromDrillingRigSequence;

public record ExcludeWellFromDrillingRigSequenceRequest(Guid SequenceId, Guid WellId) : IRequest;