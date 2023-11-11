using MongoDB.Driver;
using ReportAPI.Entities;

namespace ReportAPI.Data
{
    public class ReportDbContext<T>
    {
        private readonly IMongoDatabase _database;

        public ReportDbContext(IConfiguration configuration)
        {
            var settings = configuration.GetSection(nameof(MongoDBSettings)).Get<MongoDBSettings>();
            var client = new MongoClient(settings.ConnectionString);
            _database = client.GetDatabase(settings.DatabaseName);
        }

        public IMongoCollection<T> Reports => _database.GetCollection<T>("Reports");
    }
}
