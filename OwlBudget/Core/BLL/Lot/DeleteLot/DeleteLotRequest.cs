using System;
using MediatR;

namespace Core.BLL.Lot.DeleteLot;

public record DeleteLotRequest(Guid LotId) : IRequest;