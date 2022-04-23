using System;
using System.Collections.Generic;
using MediatR;

namespace Core.BLL.DrillingRigSequence.GetProjectDrillingRigSequences;

public record GetProjectDrillingRigSequencesRequest(Guid ProjectId) : IRequest<List<Domain.DrillingRigSequence>>;