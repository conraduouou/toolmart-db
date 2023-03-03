using ToolMart.Models;
using ToolMart.Services;
using Microsoft.AspNetCore.Mvc;

namespace ToolMart.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserService _service;
    public UsersController(UserService service) => _service = service;

    [HttpGet]
    public async Task<List<User>> Get() => await _service.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<User>> Get(string id)
    {
        var data = await _service.GetAsync(id);
        if (data is null) return NotFound();
        return data;
    }

    [HttpGet("api/[controller]/email/{email}")]
    public async Task<ActionResult<User>> GetByEmail(string email)
    {
        var data = await _service.GetByEmailAsync(email);
        if (data is null) return NotFound();
        return data;
    }

    [HttpPost]
    public async Task<IActionResult> Post(User newData)
    {
        await _service.CreateAsync(newData);
        return CreatedAtAction(nameof(Get), new { id = newData.Id }, newData);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, User updatedData)
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