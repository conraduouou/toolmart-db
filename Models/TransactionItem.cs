using MongoDB.Bson.Serialization.Attributes;

namespace ToolMart.Models;

public class TransactionItem : Model
{
    [BsonId]
    public string TransactionId { get; set; } = null!;

    [BsonId]
    public string ItemId { get; set; } = null!;

    public string? ItemColor { get; set; }

    public int ItemQuantity { get; set; }
}