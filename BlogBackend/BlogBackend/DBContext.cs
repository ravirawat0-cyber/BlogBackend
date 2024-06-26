using BlogBackend.Models;
using MongoDB.Driver;

namespace BlogBackend;

public class DBContext
{
    private readonly IMongoDatabase _database;

    public DBContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetConnectionString("MongoDB"));
        _database = client.GetDatabase(configuration["DatabaseName"]);
    }

    public IMongoCollection<Blog> Blog => _database.GetCollection<Blog>("Blog");
}