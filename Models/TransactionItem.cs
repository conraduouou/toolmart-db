using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ToolMart.Models;

public class TransactionItem : Model
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string TransactionId { get; set; } = null!;

    [BsonRepresentation(BsonType.ObjectId)]
    public string ItemId { get; set; } = null!;

    public string? ItemColor { get; set; }

    public int ItemQuantity { get; set; }
}