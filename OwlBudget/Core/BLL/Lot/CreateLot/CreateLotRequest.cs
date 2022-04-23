using System;
using MediatR;

namespace Core.BLL.Lot.CreateLot;

public record CreateLotRequest(Guid ProjectId) : IRequest;