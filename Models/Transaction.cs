using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ToolMart.Models;

public class Transaction : Model
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; } = null!;
    public DateTime Date { get; set; }
    public string PaymentMethod { get; set; } = null!;
    public int TotalQuantity { get; set; }
    public decimal Price { get; set; }
}