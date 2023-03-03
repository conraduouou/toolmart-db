namespace ToolMart.Models;

public class CartItemsCollectionSettings {
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string CartItemsCollectionName { get; set; } = null!;
}