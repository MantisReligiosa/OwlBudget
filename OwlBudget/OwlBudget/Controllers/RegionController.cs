using AutoMapper;
using Core.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OwlBudget.Models.TableResponse;

namespace OwlBudget.Controllers;

[Authorize]
[Route("api/region")]
public class RegionController : DataCatalogController<Region, CatalogItem>
{
    public RegionController(IMediator mediator, ILogger<RegionController> logger, IMapper mapper)
        : base(mediator, logger, mapper)
    {
    }
}