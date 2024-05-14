using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AttendanceTracker.Domain
{
    public class ClassRoom
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int ClassRoomId { get; set; }

        public string Name { get; set; }
    }
}
