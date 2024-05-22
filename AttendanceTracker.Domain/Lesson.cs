using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AttendanceTracker.Domain
{
    public class Lesson
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid LessonId { get; set; }
        public int ProfessorId { get; set; }
        public int SubjectId { get; set; }
        public int ClassRoomId { get; set; }
        public bool Synced { get; set; }
        public bool Deleted { get; set; }
        public DateTime Time { get; set; }  
        public Professor? Professor { get; set; }
        public Subject? Subject { get; set; }          
        public ClassRoom? ClassRoom { get; set; }


    }
}
