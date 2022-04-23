using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.BLL.DataCatalog.GetById;
using Core.BLL.Project.CreateProject;
using Core.BLL.Project.DeleteProject;
using Core.Domain;
using Core.Tools;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OwlBudget.Models;
using OwlBudget.Models.TableResponse;

namespace OwlBudget.Controllers.ProjectControllers;

[Authorize]
[Route("api/project")]
public class ProjectController : DataCatalogController<Project, ProjectCatalogItem>
{
    public ProjectController(IMediator mediator,
        ILogger<ProjectController> logger,
        IMapper mapper)
        : base(mediator, logger, mapper)
    {
    }

    [HttpPut]
    [Route("new")]
    public async Task<IActionResult> NewProjectAsync()
    {
        try
        {
            var userId = GetUserId();
            var project = await Mediator.Send(new CreateProjectRequest(userId));
            var response = Mapper.Map<Project, ProjectParamsResponse>(project);
            return Ok(response);
        }
        catch (Exception ex)
        {
            const string message = "Ошибка создания проекта";
            Logger.LogError(ex, message);
            return Problem(title: message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> OpenProjectAsync(ByIdRequest request)
    {
        return await OpenProjectAsync(request, false);
    }

    [HttpHead]
    public async Task<IActionResult> OpenProjectHeadAsync(ByIdRequest request)
    {
        return await OpenProjectAsync(request, true);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteProjectAsync(ByIdRequest request)
    {
        try
        {
            await Mediator.Send(new DeleteProjectRequest(request.Id));
            return NoContent();
        }
        catch (Exception ex)
        {
            const string message = "Ошибка удаления проекта";
            Logger.LogError(ex, message);
            return Problem(title: message);
        }
    }

    [HttpGet]
    [Route("contractTypes")]
    public IActionResult GetContractTypes()
    {
        try
        {
            var response = EnumExtension
                .GetEnumItems<ContractTypes>()
                .Select(item => new ContractTypesResponseItem {Description = item.Description, Id = (int) item.Value})
                .ToList();
            return Ok(response);
        }
        catch (Exception ex)
        {
            const string message = "Ошибка загрузки типов контрактов";
            Logger.LogError(ex, message);
            return Problem(title: message);
        }
    }

    private async Task<IActionResult> OpenProjectAsync(ByIdRequest request, bool returnHeaderOnly)
    {
        try
        {
            if (returnHeaderOnly)
                return Ok();

            var project = await Mediator.Send(new GetByIdRequest<Project>(request.Id));
            if (project == null) return NotFound();
            var response = Mapper.Map<Project, ProjectParamsResponse>(project);
            return Ok(response);
        }
        catch (Exception ex)
        {
            const string message = "Ошибка загрузки проекта";
            Logger.LogError(ex, message);
            return Problem(title: message);
        }
    }
}