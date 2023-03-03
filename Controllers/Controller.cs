using ToolMart.Models;
using ToolMart.Services;
using Microsoft.AspNetCore.Mvc;

namespace ToolMart.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Controller<T> : ControllerBase where T : Model
{
    private readonly Service<T> _service;
    public Controller(Service<T> service) => _service = service;

    [HttpGet]
    public async Task<List<T>> Get() => await _service.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<T>> Get(string id)
    {
        var data = await _service.GetAsync(id);
        if (data is null) return NotFound();
        return data;
    }

    [HttpPost]
    public async Task<IActionResult> Post(T newData)
    {
        await _service.CreateAsync(newData);
        return CreatedAtAction(nameof(Get), new { id = newData.Id }, newData);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, T updatedData)
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

[ApiController]
[Route("api/[controller]")]
public class ItemsController : Controller<Item>
{
    public ItemsController(Service<Item> service) : base(service) { }
}


[ApiController]
[Route("api/[controller]")]
public class UsersController : Controller<User>
{
    public UsersController(Service<User> service) : base(service) { }
}

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : Controller<Transaction>
{
    public TransactionsController(Service<Transaction> service) : base(service) { }
}