using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Core.BLL.DrillingRigSequence.ChangeDrillingRigSequenceName;
using Core.BLL.DrillingRigSequence.GetProjectDrillingRigSequences;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OwlBudget.Models;
using OwlBudget.Models.ProjectObjectsModels;

namespace OwlBudget.Controllers.ProjectControllers;

[Authorize]
[Route("api/project/drillingRigSequences")]
public class ProjectDrillingRigSequencesController : Controller
{
    private readonly ILogger<ProjectDrillingRigSequencesController> _logger;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public ProjectDrillingRigSequencesController(ILogger<ProjectDrillingRigSequencesController> logger,
        IMapper mapper,
        IMediator mediator)
    {
        _logger = logger;
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> LoadProjectDrillingRigSequences(ByIdRequest request)
    {
        try
        {
            var drillingRigSequences = await _mediator.Send(new GetProjectDrillingRigSequencesRequest(request.Id));
            var response = _mapper.Map<List<DrillingRigSequence>>(drillingRigSequences);
            return Ok(response);
        }
        catch (Exception ex)
        {
            const string message = "Ошибка загрузки последовательностей бурения проекта";
            _logger.LogError(ex, message);
            return Problem(title: message);
        }
    }

    [HttpPatch]
    [Route("moveToSequence")]
    public async Task<IActionResult> MoveWellToSequence([FromBody] MoveWellToSequenceRequest request)
    {
        if (request == null) return Ok();
        try
        {
            await _mediator.Send(
                new Core.BLL.DrillingRigSequence.MoveWellToSequence.MoveWellToSequenceRequest(request.CurrentSequenceId,
                    request.NewSequenceId, request.WellId, request.Order));
            return Ok();
        }
        catch (Exception ex)
        {
            const string message = "Ошибка переноса скважины в другую последовательностей бурения";
            _logger.LogError(ex, message);
            return Problem(title: message);
        }
    }

    [HttpPatch]
    [Route("name")]
    public async Task<IActionResult> RenameSequence([FromBody] UpdateParamValue<Guid, string> request)
    {
        try
        {
            var result =
                await _mediator.Send(new ChangeDrillingRigSequenceNameRequest(request.Value, request.ObjectId));
            var response = new OperationResponse(result);
            return Ok(response);
        }
        catch (Exception ex)
        {
            const string message = "Ошибка смены наименования последовательности";
            _logger.LogError(ex, message);
            return Problem(title: message);
        }
    }

    [HttpPost]
    [Route("moveToNewSequence")]
    public async Task<IActionResult> MoveToNewSequence(MoveWellToNewSequenceRequest request)
    {
        try
        {
            await _mediator.Send(
                new Core.BLL.DrillingRigSequence.MoveWellToNewSequence.MoveWellToNewSequenceRequest(request.ScenarioId,
                    request.WellId));
            return Ok();
        }
        catch (Exception ex)
        {
            const string message = "Ошибка переноса скважины в новую последовательностей бурения";
            _logger.LogError(ex, message);
            return Problem(title: message);
        }
    }
}