﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace AttendanceTracker.Domain
{
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string Index { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public string Password { get; set; }
    }
}
