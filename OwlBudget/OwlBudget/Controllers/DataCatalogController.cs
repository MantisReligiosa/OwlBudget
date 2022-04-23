using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.BLL.DataCatalog.GetById;
using Core.BLL.DataCatalog.GetList;
using Core.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OwlBudget.Models;
using OwlBudget.Models.TableResponse;

namespace OwlBudget.Controllers;

public abstract class DataCatalogController<TDomain, TCatalogItem> : Controller
    where TDomain : NamedIdentity
    where TCatalogItem : CatalogItem, new()
{
    protected readonly ILogger Logger;
    protected readonly IMapper Mapper;
    protected readonly IMediator Mediator;

    protected DataCatalogController(IMediator mediator, ILogger logger, IMapper mapper)
    {
        Mediator = mediator;
        Logger = logger;
        Mapper = mapper;
    }

    [HttpGet]
    [Route("catalogItem")]
    public async Task<IActionResult> GetCatalogItemByIdAsync(Guid id)
    {
        try
        {
            var item = await Mediator.Send(new GetByIdRequest<TDomain>(id));
            var response = new NamedItemListItemResponse {Id = id, Name = item.Name};
            return Ok(response);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Ошибка загрузки элемента справочника {Catalog}", typeof(TDomain));
            return Problem(title: "Ошибка загрузки элемента справочника");
        }
    }

    [HttpGet]
    [Route("catalog")]
    public async Task<IActionResult> GetCatalogAsync(CatalogRequest catalogRequest)
    {
        try
        {
            var listModal = await Mediator.Send(new GetListRequest<TDomain>(GetUserId(), catalogRequest.Filter,
                catalogRequest.SortBy, catalogRequest.CurrentPage, catalogRequest.PerPage, catalogRequest.SortDesc));
            var result = new TableResponse<TCatalogItem>
            {
                Page = listModal.Page,
                PerPage = listModal.PerPage,
                TotalRows = listModal.TotalRows,
                Rows = Mapper.Map<List<TDomain>, List<TCatalogItem>>(listModal.Items)
            };
            return Ok(result);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Ошибка получения справочника {Catalog}", typeof(TDomain));
            return Problem(title: "Ошибка загрузки справочника");
        }
    }

    protected Guid GetUserId()
    {
        return Guid.Parse(User.Claims.First(c => c.Type == "Id").Value);
    }
}