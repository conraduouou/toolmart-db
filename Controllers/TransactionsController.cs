using ToolMart.Models;
using ToolMart.Services;
using Microsoft.AspNetCore.Mvc;

namespace ToolMart.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase 
{
    private readonly TransactionService _service;
    public TransactionsController(TransactionService service) => _service = service;

    [HttpGet]
    public async Task<List<Transaction>> Get() => await _service.GetAsync();

    [HttpGet("{userId:length(24)}")]
    public async Task<ActionResult<List<Transaction>>> Get(string userId)
    {
        var data = await _service.GetAsync(userId);
        if (data is null) return NotFound();
        return data;
    }

    [HttpGet("{userId:length(24)}/{id:length(24)}")]
    public async Task<ActionResult<Transaction>> Get(string id, string userId)
    {
        var data = await _service.GetAsync(id, userId);
        if (data is null) return NotFound();
        return data;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Transaction newData)
    {
        await _service.CreateAsync(newData);
        return CreatedAtAction(nameof(Get), new { id = newData.Id }, newData);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, string userId, Transaction updatedData)
    {
        var data = await _service.GetAsync(id, userId);
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