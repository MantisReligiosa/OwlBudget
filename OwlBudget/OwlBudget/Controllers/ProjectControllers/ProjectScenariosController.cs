using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Core.BLL.Scenario.GetProjectScenarios;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OwlBudget.Models;
using OwlBudget.Models.ProjectObjectsModels;

namespace OwlBudget.Controllers.ProjectControllers;

[Authorize]
[Route("api/project/scenarios")]
public class ProjectScenariosController : Controller
{
    private readonly ILogger<ProjectScenariosController> _logger;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public ProjectScenariosController(IMediator mediator,
        ILogger<ProjectScenariosController> logger,
        IMapper mapper)
    {
        _mediator = mediator;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> LoadProjectScenarios(ByIdRequest request)
    {
        try
        {
            var scenarios = await _mediator.Send(new GetProjectScenariosRequest(request.Id));
            var response = _mapper.Map<List<Scenario>>(scenarios);
            return Ok(response);
        }
        catch (Exception ex)
        {
            const string message = "Ошибка загрузки сценариев проекта";
            _logger.LogError(ex, message);
            return Problem(title: message);
        }
    }
}