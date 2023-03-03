using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ToolMart.Models;

namespace ToolMart.Services;

public class TransactionItemService
{
    private readonly IMongoCollection<TransactionItem> _collection;

    public TransactionItemService(IOptions<TransactionItemsCollectionSettings> databaseSettings)
    {
        var connectionString = Environment.GetEnvironmentVariable("MONGODB_URI");
        var localString = databaseSettings.Value.ConnectionString;

        databaseSettings.Value.ConnectionString = connectionString != null ? connectionString : localString;

        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

        _collection = mongoDatabase.GetCollection<TransactionItem>(databaseSettings.Value.TransactionItemsCollectionName);
    }

    public async Task<List<TransactionItem>> GetAsync(string transactionId) 
        => await _collection.Find(x => x.TransactionId == transactionId).ToListAsync();
    public async Task<TransactionItem> GetAsync(string transactionId, string itemId, string id) 
        => await _collection.Find(
            x => x.ItemId == itemId && x.TransactionId == transactionId && x.Id == id)
            .FirstOrDefaultAsync();
    public async Task CreateAsync(TransactionItem newItem) => await _collection.InsertOneAsync(newItem);
    public async Task UpdateQuantityAsync(string transactionId, string itemId, string id, int quantity)
        => await _collection.UpdateOneAsync(
            x => x.TransactionId == transactionId && x.ItemId == itemId && x.Id == id,
                Builders<TransactionItem>.Update.Set("ItemQuantity", quantity));
    public async Task RemoveAsync(string transactionId, string itemId, string id) 
        => await _collection.DeleteOneAsync(
            x => x.TransactionId == transactionId && x.ItemId == itemId && x.Id == id);
}