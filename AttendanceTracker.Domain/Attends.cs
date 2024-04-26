using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AttendanceTracker.Domain
{
    public class Attends
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid AttendsId { get; set; }
        public Guid LessonId { get; set; }
        public string Index { get; set; }
        public bool Synced { get; set; }
        public Lesson? Lesson { get; set; }
        public Student? Student { get; set; }    
    }
}
