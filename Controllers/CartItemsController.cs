using ToolMart.Models;
using ToolMart.Services;
using Microsoft.AspNetCore.Mvc;

namespace ToolMart.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartItemsController : ControllerBase
{
    private readonly CartItemService _service;
    public CartItemsController(CartItemService service) => _service = service;

    [HttpGet("{userId:length(24)}")]
    public async Task<List<CartItem>> Get(string userId) => await _service.GetAsync(userId);

    [HttpPost]
    public async Task<IActionResult> Post(CartItem newItem)
    {
        var data = await _service.GetAsync(newItem.UserId);

        if (data is null)
        {
            await _service.CreateAsync(newItem);
            return CreatedAtAction(nameof(Post), newItem);
        }

        foreach (var item in data)
        {
            if (item.ItemId == newItem.ItemId && item.ItemColor == newItem.ItemColor)
            {
                await _service.UpdateQuantity(item.Id!, item.UserId, item.ItemQuantity + newItem.ItemQuantity);
                return CreatedAtAction(nameof(Post), newItem);
            }
        }

        return BadRequest();
    }

    [HttpPatch]
    [Route("/api/[controller]/{id?}/{userId?}/{color}")]
    public async Task<IActionResult> Patch(string id = null!, string userId = null!, string color = null!)
    {
        if (id is null) return BadRequest();
        if (userId is null) return BadRequest();
        if (color is null) return BadRequest();

        var data = await _service.GetAsync(id, userId);
        if (data is null) return NotFound();
        await _service.UpdateColor(id, userId, color);
        return NoContent();
    }

    [HttpPatch]
    [Route("/api/[controller]/{id?}/{userId?}/{quantity}")]
    public async Task<IActionResult> Patch(string id = null!, string userId = null!, int? quantity = null!)
    {
        if (id is null) return BadRequest();
        if (userId is null) return BadRequest();
        if (quantity is null) return BadRequest();

        var data = await _service.GetAsync(id, userId);
        if (data is null) return NotFound();
        await _service.UpdateQuantity(id, userId, (int)quantity);
        return NoContent();
    }

    [HttpDelete]
    [Route("/api/[controller]/{id?}/{userId?}")]
    public async Task<IActionResult> Delete(string id, string userId)
    {
        if (id is null) return BadRequest();
        if (userId is null) return BadRequest();

        var data = await _service.GetAsync(id, userId);
        if (data is null) return NotFound();
        await _service.RemoveAsync(id, userId);
        return NoContent();
    }
}