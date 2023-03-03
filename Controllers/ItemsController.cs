using ToolMart.Models;
using ToolMart.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly ItemService _service;
    public ItemsController(ItemService service) => _service = service;

    [HttpGet]
    public async Task<List<Item>> Get() => await _service.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Item>> Get(string id)
    {
        var data = await _service.GetAsync(id);
        if (data is null) return NotFound();
        return data;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Item newData)
    {
        await _service.CreateAsync(newData);
        return CreatedAtAction(nameof(Get), new { id = newData.Id }, newData);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Item updatedData)
    {
        var data = await _service.GetAsync(id);
        if (data is null) return NotFound();
        updatedData.Id = data.Id;
        await _service.UpdateAsync(id, updatedData);
        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var data = await _service.GetAsync(id);
        if (data is null) return NotFound();
        await _service.RemoveAsync(id);
        return NoContent();
    }
}