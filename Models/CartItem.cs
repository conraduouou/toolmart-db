using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ToolMart.Models;

public class CartItem : Model
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; } = null!;

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ItemId {get; set; } = null!;

    [BsonId]
    public string ItemColor { get; set; } = null!;

    [BsonId]
    public int ItemQuantity { get; set; }
}