using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ToolMart.Models;

namespace ToolMart.Services;

public class UserService
{
    private readonly IMongoCollection<User> _collection;

    public UserService(IOptions<UsersCollectionSettings> databaseSettings)
    {
        var connectionString = Environment.GetEnvironmentVariable("MONGODB_URI");
        var localString = databaseSettings.Value.ConnectionString;

        databaseSettings.Value.ConnectionString = connectionString != null ? connectionString : localString; 
        
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

        _collection = mongoDatabase.GetCollection<User>(databaseSettings.Value.UsersCollectionName);
    }

    public async Task<List<User>> GetAsync() => await _collection.Find(_ => true).ToListAsync(); 
    public async Task<User?> GetAsync(string id) => await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    public async Task CreateAsync(User newValue) => await _collection.InsertOneAsync(newValue);
    public async Task UpdateAsync(string id, User updatedItem) => await _collection.ReplaceOneAsync(x => x.Id == id, updatedItem);
    public async Task RemoveAsync(string id) => await _collection.DeleteOneAsync(x => x.Id == id);
}