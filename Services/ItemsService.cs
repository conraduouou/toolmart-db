using ToolMart.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ToolMart.Services;

public class ItemsService
{
    private readonly IMongoCollection<Item> _itemsCollection;

    public ItemsService(IOptions<ItemsDatabaseSettings> databaseSettings)
    {
        var connectionString = Environment.GetEnvironmentVariable("MONGODB_URI");
        var defaultString = databaseSettings.Value.ConnectionString;

        databaseSettings.Value.ConnectionString = connectionString != null ? connectionString : defaultString; 
        
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        _itemsCollection = mongoDatabase.GetCollection<Item>(databaseSettings.Value.ItemsCollectionName);
    }

    public async Task<List<Item>> GetAsync() => await _itemsCollection.Find(_ => true).ToListAsync(); 
    public async Task<Item?> GetAsync(string id) => await _itemsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
    public async Task CreateAsync(Item newItem) => await _itemsCollection.InsertOneAsync(newItem);
    public async Task UpdateAsync(string id, Item updatedItem) => await _itemsCollection.ReplaceOneAsync(x => x.Id == id, updatedItem);
    public async Task RemoveAsync(string id) => await _itemsCollection.DeleteOneAsync(x => x.Id == id);
}