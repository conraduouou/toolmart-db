using ToolMart.Models;
using ToolMart.Services;
using Microsoft.AspNetCore.Mvc;

namespace ToolMart.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly ReviewService _service;
    public ReviewsController(ReviewService service) => _service = service;

    [HttpGet("{itemId:length(24)}")]
    public async Task<List<Review>> Get(string itemId) => await _service.GetAsync(itemId);

    [HttpPost]
    public async Task<IActionResult> Post(Review newReview)
    {
        await _service.CreateAsync(newReview);
        return CreatedAtAction(nameof(Get), new { id = newReview.Id }, newReview);
    }

    [HttpPatch]
    [Route("/api/[controller]/comment/{userId?}/{itemId?}/{id?}/{comment?}")]
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

    [HttpPatch]
    [Route("/api/[controller]/rating/{userId?}/{itemId?}/{id?}/{rating?}")]
    public async Task<IActionResult> Patch(string userId = null!, string itemId = null!, string id = null!, int rating = 0)
    {
        if (userId is null) return BadRequest();
        if (itemId is null) return BadRequest();
        if (id is null) return BadRequest();

        var data = await _service.GetAsync(userId, itemId, id);
        if (data is null) return NotFound();
        await _service.UpdateRatingAsync(userId, itemId, id, rating);
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