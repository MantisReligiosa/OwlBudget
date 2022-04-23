using AutoMapper;
using Core.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OwlBudget.Models.TableResponse;

namespace OwlBudget.Controllers;

[Authorize]
[Route("api/wellconstruction")]
public class WellConstructionController : DataCatalogController<WellConstruction, CatalogItem>
{
    public WellConstructionController(IMediator mediator, ILogger<WellConstructionController> logger, IMapper mapper)
        : base(mediator, logger, mapper)
    {
    }
}