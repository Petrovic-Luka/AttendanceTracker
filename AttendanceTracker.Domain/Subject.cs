using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace AttendanceTracker.Domain
{

    public class Subject
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int SubjectId { get; set; }

        public string Name { get; set; }
    }
}
