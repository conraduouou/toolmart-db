using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ToolMart.Models;

public class Item
{
    public Item(string? Name) {
        this.Name = Name;
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string? Name { get; set; } = null;
    public string? Details {get;set;}
    public List<string>? Materials {get;set;}
    public List<string>? Colors {get; set;}
    public int Quantity { get; set; }
    public decimal Price { get; set; }

}