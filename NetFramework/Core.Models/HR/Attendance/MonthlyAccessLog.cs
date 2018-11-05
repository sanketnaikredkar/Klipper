using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Core.Models.HR.Attendance
{
    public class MonthlyAccessLog
    {
        [BsonId]
        public ObjectId _objetId { get; set; }

        [BsonDateTimeOptions]
        public DateTime Month { get; set; }

        public List<AccessEvent> AccessEvents { get; set; }
    }
}
