using System;
using System.Threading.Tasks;
using AutoMapper;
using Core.BLL.DataCatalog.GetById;
using Core.BLL.Project.ChangeProjectDescription;
using Core.BLL.Project.ChangeProjectLocation;
using Core.BLL.Project.ChangeProjectName;
using Core.BLL.Project.ChangeProjectRegion;
using Core.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OwlBudget.Models;

namespace OwlBudget.Controllers.ProjectControllers;

[Authorize]
[Route("api/project/params")]
public class ProjectParamsController : Controller
{
    private readonly ILogger<ProjectParamsController> _logger;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public ProjectParamsController(
        IMediator mediator,
        ILogger<ProjectParamsController> logger,
        IMapper mapper)
    {
        _mediator = mediator;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> LoadProjectParams(ByIdRequest request)
    {
        try
        {
            var project = await _mediator.Send(new GetByIdRequest<Project>(request.Id));
            if (project == null) return NotFound();
            var response = _mapper.Map<ProjectParamsResponse>(project);
            return Ok(response);
        }
        catch (Exception ex)
        {
            const string message = "Ошибка загрузки проекта";
            _logger.LogError(ex, message);
            return Problem(title: message);
        }
    }

    [HttpPatch]
    [Route("name")]
    public async Task<IActionResult> ChangeName([FromBody] UpdateParamValue<Guid, string> request)
    {
        try
        {
            var result = await _mediator.Send(new ChangeProjectNameRequest(request.Value, request.ObjectId));
            var response = new OperationResponse(result);
            return Ok(response);
        }
        catch (Exception ex)
        {
            const string message = "Ошибка смены наименования проекта";
            _logger.LogError(ex, message);
            return Problem(title: message);
        }
    }

    [HttpPatch]
    [Route("location")]
    public async Task<IActionResult> ChangeLocation([FromBody] UpdateParamValue<Guid, string> request)
    {
        try
        {
            var result = await _mediator.Send(new ChangeProjectLocationRequest(request.Value, request.ObjectId));
            var response = new OperationResponse(result);
            return Ok(response);
        }
        catch (Exception ex)
        {
            const string message = "Ошибка смены региона проекта";
            _logger.LogError(ex, message);
            return Problem(title: message);
        }
    }

    [HttpPatch]
    [Route("description")]
    public async Task<IActionResult> ChangeDescription([FromBody] UpdateParamValue<Guid, string> request)
    {
        try
        {
            var result = await _mediator.Send(new ChangeProjectDescriptionRequest(request.ObjectId, request.Value));
            var response = new OperationResponse(result);
            return Ok(response);
        }
        catch (Exception ex)
        {
            const string message = "Ошибка смены описания проекта";
            _logger.LogError(ex, message);
            return Problem(title: message);
        }
    }

    [HttpPatch]
    [Route("region")]
    public async Task<IActionResult> ChangeRegion([FromBody] UpdateParamValue<Guid, Guid> request)
    {
        try
        {
            var result = await _mediator.Send(new ChangeProjectRegionRequest(request.ObjectId, request.Value));
            var response = new OperationResponse(result);
            return Ok(response);
        }
        catch (Exception ex)
        {
            const string message = "Ошибка смены региона проекта";
            _logger.LogError(ex, message);
            return Problem(title: message);
        }
    }
}