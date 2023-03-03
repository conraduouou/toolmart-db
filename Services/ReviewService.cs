using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using ToolMart.Models;

namespace ToolMart.Services;

public class ReviewService
{
    private readonly IMongoCollection<Review> _collection;

    public ReviewService(IOptions<ReviewsCollectionSettings> databaseSettings)
    {
        var connectionString = Environment.GetEnvironmentVariable("MONGODB_URI");
        var localString = databaseSettings.Value.ConnectionString;

        databaseSettings.Value.ConnectionString = connectionString != null ? connectionString : localString;

        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

        _collection = mongoDatabase.GetCollection<Review>(databaseSettings.Value.ReviewsCollectionName);
    }

    public async Task<List<Review>> GetAsync(string itemId) 
        => await _collection.Find(x => x.ItemId == itemId).ToListAsync();
    public async Task<Review> GetAsync(string userId, string itemId, string id) 
        => await _collection.Find(
            x => x.ItemId == itemId && x.UserId == userId && x.Id == id)
            .FirstOrDefaultAsync();
    public async Task CreateAsync(Review newReview) => await _collection.InsertOneAsync(newReview);
    public async Task<UpdateResult> UpdateCommentAsync(string userId, string itemId, string id, string comment)
        => await _collection.UpdateOneAsync(
            x => x.UserId == userId && x.ItemId == itemId && x.Id == id,
                Builders<Review>.Update.Set("UserComment", comment));
    public async Task RemoveAsync(string userId, string itemId, string id) 
        => await _collection.DeleteOneAsync(
            x => x.UserId == userId && x.ItemId == itemId && x.Id == id);
}