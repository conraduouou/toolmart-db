namespace ToolMart.Models;

public class User : Model
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}