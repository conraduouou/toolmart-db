namespace ToolMart.Models;

public class TransactionItemsCollectionSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string TransactionItemsCollectionName { get; set; } = null!;
}