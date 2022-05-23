using Infrastructure.Definitions;
using Infrastructure.Modules.Items.Entities;
using Infrastructure.Modules.Items.Requests;
using Infrastructure.Modules.Items.Servises;
using Infrastructure.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Items;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ItemsController : BaseController
{
    private readonly IItemService _itemService;
    public ItemsController(IItemService itemService)
    {
        _itemService = itemService;
    }
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll([FromQuery] PaginationRequest request)
    {
        PaginationResponse<Item> items = await _itemService.GetAllAsync(request);
        return Ok(items, Messages.Items.GetSuccessfully);
    }

    [HttpGet("{itemId:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetDetailAsync(Guid itemId)
    {
        (Item? item, string? errorMessage) = await _itemService.GetDetailAsync(itemId);
        if (errorMessage is not null) return BadRequest(errorMessage);
        return Ok(item, Messages.Items.GetSuccessfully);
    }


    [HttpPut]
    public async Task<IActionResult> Update([FromQuery] Guid id, [FromBody] UpdateItemRequest request)
    {
        (Item? item, string? errorMessage) = await _itemService.GetByIdAsync(id);
        if (errorMessage is not null) return BadRequest(errorMessage);
        await _itemService.UpdateAsync(item!, request);
        return Ok(item, Messages.Items.UpdateSuccessfully);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(List<Guid> itemIds)
    {
        string? errorMessage = await _itemService.DeleteRangeAsync(itemIds);
        if (errorMessage is not null) return BadRequest(errorMessage);
        return Ok(null, Messages.Items.DeleteSuccessfully);
    }
}