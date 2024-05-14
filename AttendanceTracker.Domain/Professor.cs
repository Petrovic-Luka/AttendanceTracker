using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace AttendanceTracker.Domain
{
    public class Professor
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int ProfessorId { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public string Password { get; set; }
    }
}
