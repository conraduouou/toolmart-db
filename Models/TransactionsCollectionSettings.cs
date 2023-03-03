namespace ToolMart.Models;

public class TransactionsCollectionSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string TransactionsCollectionName { get; set; } = null!;
}