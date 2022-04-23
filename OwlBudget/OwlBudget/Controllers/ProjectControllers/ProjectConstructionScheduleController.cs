using System;
using System.Threading.Tasks;
using Core.BLL.WellBuildingSchedule.CreateWellBuildingScheduleItem;
using Core.BLL.WellBuildingSchedule.DeleteWellBuildingScheduleItem;
using Core.BLL.WellBuildingSchedule.MakeSchedule;
using Core.BLL.WellBuildingSchedule.UpdateWellBuildingScheduleItem;
using Core.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OwlBudget.Models;

namespace OwlBudget.Controllers.ProjectControllers;

[Authorize]
[Route("api/project/constructionSchedule")]
public class ProjectConstructionScheduleController : Controller
{
    private readonly ILogger<ProjectConstructionScheduleController> _logger;
    private readonly IMediator _mediator;

    public ProjectConstructionScheduleController(
        IMediator mediator,
        ILogger<ProjectConstructionScheduleController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> LoadConstructionSchedule(ByIdRequest request)
    {
        try
        {
            var response = await _mediator.Send(new MakeScheduleRequest(request.Id));
            return Ok(response);
        }
        catch (Exception ex)
        {
            const string message = "Ошибка загрузки графика строительства";
            _logger.LogError(ex, message);
            return Problem(title: message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateSchedule(NewConstructionScheduleRequest request)
    {
        try
        {
            await _mediator.Send(new CreateWellBuildingScheduleItemRequest(request.ProjectId,
                request.ScenarioId, request.WellId, request.TemplateType));
            return Ok();
        }
        catch (Exception ex)
        {
            const string message = "Ошибка создания элемента графика строительства";
            _logger.LogError(ex, message);
            return Problem(title: message);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteSchedule(ByIdRequest request)
    {
        try
        {
            await _mediator.Send(new DeleteWellBuildingScheduleItemRequest(request.Id));
            return NoContent();
        }
        catch (Exception ex)
        {
            const string message = "Ошибка удаления элемента графика строительства";
            _logger.LogError(ex, message);
            return Problem(title: message);
        }
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateSchedule(UpdateScheduleRequest request)
    {
        try
        {
            await _mediator.Send(new UpdateWellBuildingScheduleItemRequest(request.Id,
                request.DurationDays,
                ConstructionScheduleTemplates.ByType(request.TemplateType)));
            return Ok();
        }
        catch (Exception ex)
        {
            const string message = "Ошибка редактирования элемента графика строительства";
            _logger.LogError(ex, message);
            return Problem(title: message);
        }
    }
}