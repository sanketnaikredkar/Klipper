using Core.Models.Employment;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.HR.Attendance
{
    public class AccessEvent
    {
        [BsonId]
        public ObjectId _objetId { get; set; }

        public int ID { get; set; }

        public int AccessPointID { get; set; }

        public string AccessPointName { get; set; }

        public string AccessPointIPAddress { get; set; }

        public int EmployeeID { get; set; }

        public string EmployeeFirstName { get; set; }

        public string EmployeeLastName { get; set; }

        [BsonDateTimeOptions]
        public DateTime EventTime { get; set; }
    }
}
