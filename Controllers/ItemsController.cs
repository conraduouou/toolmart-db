using ToolMart.Models;
using ToolMart.Services;
using Microsoft.AspNetCore.Mvc;

namespace ToolMart.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : Controller<Item>
{
    public ItemsController(Service<Item> service) : base(service) { }
}