using AttendanceTracker.Domain;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace AttendanceTracker.DataAccess.MongoDb
{
    public class MongoDbConnection
    {
        private readonly IConfiguration _config;
        private readonly IMongoDatabase _db;
        public string DbName { get; private set; }
        public string LessonCollectionName { get; private set; } = "lessons";
        public string StatusCollectionName { get; private set; } = "statuses";
        public string UserCollectionName { get; private set; } = "users";
        public string SuggestionCollectionName { get; private set; } = "suggestions";

        public IMongoCollection<Lesson> LessonCollection { get; private set; }
        public MongoClient Client { get; private set; }

        public MongoDbConnection(IConfiguration config)
        {
            _config = config;
            Client = new MongoClient(_config.GetConnectionString("MongoDbConnection"));
            DbName = _config["MongoDatabaseName"];
            _db = Client.GetDatabase(DbName);

            LessonCollection = _db.GetCollection<Lesson>(LessonCollectionName);
        }
    }
}
