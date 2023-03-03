using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ToolMart.Models;

namespace ToolMart.Services;

public class Service<T> where T : Model
{
    private readonly IMongoCollection<T> _collection;

    public Service(IOptions<CollectionSettings> databaseSettings)
    {
        var connectionString = Environment.GetEnvironmentVariable("MONGODB_URI");
        var localString = databaseSettings.Value.ConnectionString;

        databaseSettings.Value.ConnectionString = connectionString != null ? connectionString : localString; 
        
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

        _collection = mongoDatabase.GetCollection<T>(databaseSettings.Value.CollectionName);
    }

    public async Task<List<T>> GetAsync() => await _collection.Find(_ => true).ToListAsync(); 
    public async Task<T?> GetAsync(string id) => await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    public async Task CreateAsync(T newValue) => await _collection.InsertOneAsync(newValue);
    public async Task UpdateAsync(string id, T updatedItem) => await _collection.ReplaceOneAsync(x => x.Id == id, updatedItem);
    public async Task RemoveAsync(string id) => await _collection.DeleteOneAsync(x => x.Id == id);
}