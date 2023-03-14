using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ToolMart.Models;

namespace ToolMart.Services;

public class TransactionService
{
    private readonly IMongoCollection<Transaction> _collection;

    public TransactionService(IOptions<TransactionsCollectionSettings> databaseSettings)
    {
        var connectionString = Environment.GetEnvironmentVariable("MONGODB_URI");
        var localString = databaseSettings.Value.ConnectionString;

        databaseSettings.Value.ConnectionString = connectionString != null ? connectionString : localString; 
        
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

        _collection = mongoDatabase.GetCollection<Transaction>(databaseSettings.Value.TransactionsCollectionName);
    }

    public async Task<List<Transaction>> GetAsync() => await _collection.Find(_ => true).ToListAsync(); 
    public async Task<List<Transaction>> GetAsync(string userId) => await _collection.Find(x => x.UserId == userId).ToListAsync(); 
    public async Task<Transaction?> GetAsync(string id, string userId) => await _collection.Find(x => x.Id == id && x.UserId == userId).FirstOrDefaultAsync();
    public async Task CreateAsync(Transaction newValue) => await _collection.InsertOneAsync(newValue);
    public async Task UpdateAsync(string id, Transaction updatedItem) => await _collection.ReplaceOneAsync(x => x.Id == id, updatedItem);
    public async Task RemoveAsync(string id) => await _collection.DeleteOneAsync(x => x.Id == id);
}