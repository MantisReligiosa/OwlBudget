using AutoMapper;
using Core.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OwlBudget.Models.TableResponse;

namespace OwlBudget.Controllers;

[Authorize]
[Route("api/drillingRig")]
public class DrillingRigController : DataCatalogController<DrillingRig, CatalogItem>
{
    public DrillingRigController(IMediator mediator, ILogger<DrillingRigController> logger, IMapper mapper)
        : base(mediator, logger, mapper)
    {
    }
}