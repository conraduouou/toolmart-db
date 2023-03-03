namespace ToolMart.Models;

public class ReviewsCollectionSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string ReviewsCollectionName { get; set; } = null!;
}