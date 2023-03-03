using ToolMart.Models;
using ToolMart.Services;
using Microsoft.AspNetCore.Mvc;

namespace ToolMart.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : Controller<User>
{
    public UsersController(Service<User> service) : base(service) { }
}