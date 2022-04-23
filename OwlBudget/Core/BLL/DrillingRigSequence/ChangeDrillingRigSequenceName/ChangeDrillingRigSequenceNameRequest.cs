using System;
using Core.Models.OperationResults;
using MediatR;

namespace Core.BLL.DrillingRigSequence.ChangeDrillingRigSequenceName;

public record ChangeDrillingRigSequenceNameRequest
    (string Name, Guid DrillingRigSequenceId) : IRequest<OperationResult>;