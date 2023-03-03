using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ToolMart.Models;

public class CartItem : Model
{
    public string UserId { get; set; } = null!;

    public string ItemId {get; set; } = null!;

    public string ItemColor { get; set; } = null!;

    public int ItemQuantity { get; set; }
}