using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Core.BLL.Cluster.ChangeClusterName;
using Core.BLL.Cluster.ChangeClusterWellType;
using Core.BLL.Cluster.CreateCluster;
using Core.BLL.Cluster.DeleteCluster;
using Core.BLL.Lot.ChangeLotName;
using Core.BLL.Lot.CreateLot;
using Core.BLL.Lot.DeleteLot;
using Core.BLL.Lot.GetProjectLots;
using Core.BLL.Well.ChangeWellConstruction;
using Core.BLL.Well.ChangeWellDrillingRig;
using Core.BLL.Well.ChangeWellName;
using Core.BLL.Well.CreateWell;
using Core.BLL.Well.DeleteWell;
using Core.BLL.Well.GetWellsHeaders;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OwlBudget.Models;
using OwlBudget.Models.ProjectObjectsModels;
using WellHeader = Core.BLL.Well.GetWellsHeaders.WellHeader;

namespace OwlBudget.Controllers.ProjectControllers;

[Authorize]
[Route("api/project/objects")]
public class ProjectObjectsController : Controller
{
    private readonly ILogger<ProjectObjectsController> _logger;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public ProjectObjectsController(ILogger<ProjectObjectsController> logger,
        IMapper mapper,
        IMediator mediator)
    {
        _logger = logger;
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> LoadProjectObjects(ByIdRequest request)
    {
        try
        {
            var lots = await _mediator.Send(new GetProjectLotsRequest(request.Id));
            var response = _mapper.Map<List<Lot>>(lots);
            return Ok(response);
        }
        catch (Exception ex)
        {
            const string message = "Ошибка загрузки проекта";
            _logger.LogError(ex, message);
            return Problem(title: message);
        }
    }

    [HttpPost]
    [Route("lot")]
    public async Task<IActionResult> CreateLot(NewLotRequest request)
    {
        try
        {
            await _mediator.Send(new CreateLotRequest(request.ProjectId));
            return Ok();
        }
        catch (Exception ex)
        {
            const string message = "Ошибка создания лота";
            _logger.LogError(ex, message);
            return Problem(title: message);
        }
    }

    [HttpDelete]
    [Route("lot")]
    public async Task<IActionResult> DeleteLot(ByIdRequest request)
    {
        try
        {
            await _mediator.Send(new DeleteLotRequest(request.Id));
            return NoContent();
        }
        catch (Exception ex)
        {
            const string message = "Ошибка удаления лота";
            _logger.LogError(ex, message);
            return Problem(title: message);
        }
    }

    [HttpPatch]
    [Route("lot/name")]
    public async Task<IActionResult> RenameLot([FromBody] UpdateParamValue<Guid, string> request)
    {
        try
        {
            var result = await _mediator.Send(new ChangeLotNameRequest(request.Value, request.ObjectId));
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

    [HttpPost]
    [Route("cluster")]
    public async Task<IActionResult> CreateCluster(NewClusterRequest request)
    {
        try
        {
            await _mediator.Send(new CreateClusterRequest(request.LotId));
            return Ok();
        }
        catch (Exception ex)
        {
            const string message = "Ошибка создания куста";
            _logger.LogError(ex, message);
            return Problem(title: message);
        }
    }

    [HttpDelete]
    [Route("cluster")]
    public async Task<IActionResult> DeleteCluster(ByIdRequest request)
    {
        try
        {
            await _mediator.Send(new DeleteClusterRequest(request.Id));
            return NoContent();
        }
        catch (Exception ex)
        {
            const string message = "Ошибка удаления куста";
            _logger.LogError(ex, message);
            return Problem(title: message);
        }
    }

    [HttpPatch]
    [Route("cluster/name")]
    public async Task<IActionResult> RenameCluster([FromBody] UpdateParamValue<Guid, string> request)
    {
        try
        {
            var result = await _mediator.Send(new ChangeClusterNameRequest(request.Value, request.ObjectId));
            var response = new OperationResponse(result);
            return Ok(response);
        }
        catch (Exception ex)
        {
            const string message = "Ошибка смены наименования куста";
            _logger.LogError(ex, message);
            return Problem(title: message);
        }
    }

    [HttpPatch]
    [Route("cluster/wellType")]
    public async Task<IActionResult> ChangeClusterWellType([FromBody] UpdateParamValue<Guid, Guid> request)
    {
        try
        {
            var result = await _mediator.Send(new ChangeClusterWellTypeRequest(request.Value, request.ObjectId));
            var response = new OperationResponse(result);
            return Ok(response);
        }
        catch (Exception ex)
        {
            const string message = "Ошибка смены наименования куста";
            _logger.LogError(ex, message);
            return Problem(title: message);
        }
    }

    [HttpPost]
    [Route("well")]
    public async Task<IActionResult> CreateWell(NewWellRequest request)
    {
        try
        {
            await _mediator.Send(new CreateWellRequest(request.ClusterId));
            return Ok();
        }
        catch (Exception ex)
        {
            const string message = "Ошибка создания скважины";
            _logger.LogError(ex, message);
            return Problem(title: message);
        }
    }

    [HttpDelete]
    [Route("well")]
    public async Task<IActionResult> DeleteWell(ByIdRequest request)
    {
        try
        {
            await _mediator.Send(new DeleteWellRequest(request.Id));
            return NoContent();
        }
        catch (Exception ex)
        {
            const string message = "Ошибка удаления скважины";
            _logger.LogError(ex, message);
            return Problem(title: message);
        }
    }

    [HttpPatch]
    [Route("well/name")]
    public async Task<IActionResult> RenameWell([FromBody] UpdateParamValue<Guid, string> request)
    {
        try
        {
            var result = await _mediator.Send(new ChangeWellNameRequest(request.Value, request.ObjectId));
            var response = new OperationResponse(result);
            return Ok(response);
        }
        catch (Exception ex)
        {
            const string message = "Ошибка смены наименования скважины";
            _logger.LogError(ex, message);
            return Problem(title: message);
        }
    }

    [HttpPatch]
    [Route("well/construction")]
    public async Task<IActionResult> ChangeWellConstructionId([FromBody] UpdateParamValue<Guid, Guid> request)
    {
        try
        {
            var result = await _mediator.Send(
                new ChangeWellConstructionRequest(request.Value, request.ObjectId));
            var response = new OperationResponse(result);
            return Ok(response);
        }
        catch (Exception ex)
        {
            const string message = "Ошибка смены наименования скважины";
            _logger.LogError(ex, message);
            return Problem(title: message);
        }
    }

    [HttpPatch]
    [Route("well/drillingrig")]
    public async Task<IActionResult> ChangeWellDrillingRigId(
        [FromBody] UpdateParamValue<ChangeWellDrillingRigIdObjectId, Guid> request)
    {
        try
        {
            var result = await _mediator.Send(new ChangeWellDrillingRigRequest(request.Value,
                request.ObjectId.ScenarioId, request.ObjectId.WellId));
            var response = new OperationResponse(result);
            return Ok(response);
        }
        catch (Exception ex)
        {
            const string message = "Ошибка смены наименования скважины";
            _logger.LogError(ex, message);
            return Problem(title: message);
        }
    }

    [HttpGet]
    [Route("well/headers")]
    public async Task<IActionResult> GetWellsHeaders(ByIdRequest request)
    {
        try
        {
            var wellsHeaders = await _mediator.Send(new GetWellsHeadersRequest(request.Id));
            var response = _mapper.Map<List<WellHeader>>(wellsHeaders);
            return Ok(response);
        }
        catch (Exception ex)
        {
            const string message = "Ошибка загрузки наименований скважин проекта";
            _logger.LogError(ex, message);
            return Problem(title: message);
        }
    }
}