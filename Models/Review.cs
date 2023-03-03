using MongoDB.Bson.Serialization.Attributes;

namespace ToolMart.Models;

public class Review : Model
{
    [BsonId]
    public string UserId { get; set; } = null!;

    [BsonId]
    public string ItemId { get; set; } = null!;

    public string? UserComment { get; set; }

    public float UserRating { get; set; }
}