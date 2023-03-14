using ToolMart.Models;
using ToolMart.Services;
using Microsoft.AspNetCore.Mvc;

namespace ToolMart.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionItemsController : ControllerBase
{
    private readonly TransactionItemService _service;
    public TransactionItemsController(TransactionItemService service) => _service = service;

    [HttpGet("{itemId:length(24)}")]
    public async Task<ActionResult<List<TransactionItem>>> Get(string transactionId) {
        var data = await _service.GetAsync(transactionId);
        if (data is null) return NotFound();
        return data;
    } 

    [HttpPost]
    public async Task<IActionResult> Post(List<TransactionItem> newItems)
    {
        await _service.CreateAsync(newItems);
        return CreatedAtAction(nameof(Get), new { transactionId = newItems[0].TransactionId }, newItems);
    }

    [HttpPatch]
    [Route("/api/[controller]/{transactionId?}/{itemId?}/{id?}/{quantity?}")]
    public async Task<IActionResult> Patch(string transactionId = null!, string itemId = null!, string id = null!, int? quantity = null!)
    {
        if (transactionId is null) return BadRequest();
        if (itemId is null) return BadRequest();
        if (id is null) return BadRequest();
        if (quantity is null) return BadRequest();

        var data = await _service.GetAsync(transactionId, itemId, id);
        if (data is null) return NotFound();
        await _service.UpdateQuantityAsync(transactionId, itemId, id, (int) quantity);
        return NoContent();
    }

    [HttpDelete]
    [Route("/api/[controller]/{transactionId?}/{itemId?}/{id?}")]
    public async Task<IActionResult> Delete(string transactionId = null!, string itemId = null!, string id = null!)
    {
        if (transactionId is null) return BadRequest();
        if (itemId is null) return BadRequest();
        if (id is null) return BadRequest();

        var data = await _service.GetAsync(transactionId, itemId, id);
        if (data is null) return NotFound();
        await _service.RemoveAsync(transactionId, itemId, id);
        return NoContent();
    }
}