using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ToolMart.Models;

namespace ToolMart.Services;

public class ItemService
{
    private readonly IMongoCollection<Item> _collection;

    public ItemService(IOptions<ItemsCollectionSettings> databaseSettings)
    {
        var connectionString = Environment.GetEnvironmentVariable("MONGODB_URI");
        var localString = databaseSettings.Value.ConnectionString;

        databaseSettings.Value.ConnectionString = connectionString != null ? connectionString : localString; 
        
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

        _collection = mongoDatabase.GetCollection<Item>(databaseSettings.Value.ItemsCollectionName);
    }

    public async Task<List<Item>> GetAsync() => await _collection.Find(_ => true).ToListAsync(); 
    public async Task<Item?> GetAsync(string id) => await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    public async Task CreateAsync(Item newValue) => await _collection.InsertOneAsync(newValue);
    public async Task UpdateAsync(string id, Item updatedItem) => await _collection.ReplaceOneAsync(x => x.Id == id, updatedItem);
    public async Task RemoveAsync(string id) => await _collection.DeleteOneAsync(x => x.Id == id);
}