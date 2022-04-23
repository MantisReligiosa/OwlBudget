using AutoMapper;
using Core.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OwlBudget.Models.TableResponse;

namespace OwlBudget.Controllers;

[Authorize]
[Route("api/welltype")]
public class WellTypeController : DataCatalogController<WellType, CatalogItem>
{
    public WellTypeController(IMediator mediator, ILogger<WellTypeController> logger, IMapper mapper)
        : base(mediator, logger, mapper)
    {
    }
}