using AttendanceTracker.Domain;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Core.Connections;

namespace AttendanceTracker.DataAccess.MongoDb
{
    public class MongoDbConnection
    {
        private readonly IConfiguration _config;
        private readonly IMongoDatabase _db;
        public string DbName { get; private set; }
        public string LessonCollectionName { get; private set; } = "lessons";
        public string AttendsCollectionName { get; private set; } = "attends";
        public string ClassroomCollectionName { get; private set; } = "classrooms";
        public string SubjectCollectionName { get; private set; } = "subjects";
        public string StudentCollectionName { get; private set; } = "students";
        public string ProfessorCollectionName { get; private set; } = "professors";

        public IMongoCollection<Lesson> LessonCollection { get; private set; }
        public IMongoCollection<Attends> AttendsCollection { get; private set; }
        public IMongoCollection<ClassRoom> ClassroomCollection { get; private set; }
        public IMongoCollection<Subject> SubjectCollection { get; private set; }
        public IMongoCollection<Student> StudentCollection { get; private set; }
        public IMongoCollection<Professor> ProfessorCollection { get; private set; }
        public MongoClient Client { get; private set; }
        private IClientSession session;
        public IClientSession Session
        {
            get
            {
                if(session == null)
                {
                    session=Client.StartSession();
                }
                return session;
            }
        }

        public MongoDbConnection(IConfiguration config)
        {
            _config = config;
            Client = new MongoClient(_config.GetConnectionString("MongoDbConnection"));
            DbName = _config["MongoDatabaseName"];
            _db = Client.GetDatabase(DbName);
            LessonCollection = _db.GetCollection<Lesson>(LessonCollectionName);
            AttendsCollection = _db.GetCollection<Attends>(AttendsCollectionName);
            ClassroomCollection = _db.GetCollection<ClassRoom>(ClassroomCollectionName);
            SubjectCollection = _db.GetCollection<Subject>(SubjectCollectionName);
            StudentCollection = _db.GetCollection<Student>(StudentCollectionName);
            ProfessorCollection = _db.GetCollection<Professor>(ProfessorCollectionName);
        }
    }
}
