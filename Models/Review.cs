using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ToolMart.Models;

public class Review : Model
{
    public string UserId { get; set; } = null!;

    public string ItemId { get; set; } = null!;

    public string? UserComment { get; set; }

    public int UserRating { get; set; }
}