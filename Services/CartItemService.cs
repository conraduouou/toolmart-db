using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ToolMart.Models;

namespace ToolMart.Services;

public class CartItemService
{
    private readonly IMongoCollection<CartItem> _collection;

    public CartItemService(IOptions<CartItemsCollectionSettings> databaseSettings)
    {
        var connectionString = Environment.GetEnvironmentVariable("MONGODB_URI");
        var localString = databaseSettings.Value.ConnectionString;

        databaseSettings.Value.ConnectionString = connectionString != null ? connectionString : localString;

        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

        _collection = mongoDatabase.GetCollection<CartItem>(databaseSettings.Value.CartItemsCollectionName);
    }

    public async Task<List<CartItem>> GetAsync(string userId) => await _collection.Find(x => x.UserId == userId).ToListAsync();
    public async Task<CartItem> GetAsync(string id, string userId)
        => await _collection.Find(
            x => x.Id == id && x.UserId == userId)
                .FirstOrDefaultAsync();

    public async Task CreateAsync(CartItem item) => await _collection.InsertOneAsync(item);
    public async Task UpdateColor(string id, string userId, string color)
        => await _collection.UpdateOneAsync(
            x => x.Id == id && x.UserId == userId,
                Builders<CartItem>.Update.Set("ItemColor", color));
    public async Task UpdateQuantity(string id, string userId, int quantity)
        => await _collection.UpdateOneAsync(
            x => x.Id == id && x.UserId == userId,
                Builders<CartItem>.Update.Set("ItemQuantity", quantity));
    public async Task RemoveAsync(string id, string userId)
        => await _collection.DeleteOneAsync(
            x => x.Id == id && x.UserId == userId);

    public async Task RemoveManyAsync(string userId) 
        => await _collection.DeleteManyAsync(x => x.UserId == userId);
}