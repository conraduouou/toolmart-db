namespace ToolMart.Models;

public class ItemsCollectionSettings {
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string ItemsCollectionName { get; set; } = null!;
}