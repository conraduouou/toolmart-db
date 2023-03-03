using ToolMart.Models;
using ToolMart.Services;
using Microsoft.AspNetCore.Mvc;

namespace ToolMart.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewController : ControllerBase
{
    private readonly ReviewServices _service;
    public ReviewController(ReviewServices service) => _service = service;

    [HttpGet("{itemId:length(24)}")]
    public async Task<List<Review>> Get(string itemId) => await _service.GetAsync(itemId);

    [HttpPost]
    public async Task<IActionResult> Post(Review newReview)
    {
        await _service.CreateAsync(newReview);
        return CreatedAtAction(nameof(Get), new { id = newReview.Id }, newReview);
    }

    [HttpPatch]
    [Route("/api/[controller]/{userId?}/{itemId?}/{id?}/{comment?}")]
    public async Task<IActionResult> Patch(string userId = null!, string itemId = null!, string id = null!, string comment = null!)
    {
        if (userId is null) return BadRequest();
        if (itemId is null) return BadRequest();
        if (id is null) return BadRequest();

        var data = await _service.GetAsync(userId, itemId, id);
        if (data is null) return NotFound();
        await _service.UpdateCommentAsync(userId, itemId, id, comment);
        return NoContent();
    }

    [HttpDelete]
    [Route("/api/[controller]/{userId?}/{itemId?}/{id?}")]
    public async Task<IActionResult> Delete(string userId = null!, string itemId = null!, string id = null!)
    {
        if (userId is null) return BadRequest();
        if (itemId is null) return BadRequest();
        if (id is null) return BadRequest();

        var data = await _service.GetAsync(userId, itemId, id);
        if (data is null) return NotFound();
        await _service.RemoveAsync(userId, itemId, id);
        return NoContent();
    }
}