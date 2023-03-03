namespace ToolMart.Models;

public class Item : Model
{
    public string? Name { get; set; } = null;
    public string? Details { get; set; }
    public List<string>? Materials { get; set; }
    public List<string>? Colors { get; set; }
    public int Left { get; set; }
    public decimal Price { get; set; }

}