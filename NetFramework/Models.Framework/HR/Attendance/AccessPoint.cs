using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Framework.HR.Attendance
{
    public class AccessPoint
    {
        [BsonId]
        public ObjectId _objetId { get; set; }

        public int ID { get; set; }

        public string Name { get; set; }

        public string IpAddress { get; set; }

    }
}
