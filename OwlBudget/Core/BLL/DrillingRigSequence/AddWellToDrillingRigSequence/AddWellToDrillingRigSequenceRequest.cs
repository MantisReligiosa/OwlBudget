using System;
using MediatR;

namespace Core.BLL.DrillingRigSequence.AddWellToDrillingRigSequence;

public record AddWellToDrillingRigSequenceRequest(Guid SequenceId, Guid WellId) : IRequest;