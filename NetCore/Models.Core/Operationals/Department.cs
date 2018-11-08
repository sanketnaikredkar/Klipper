using Models.Core.Employment;
using Models.Core.Helpers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Models.Core.Operationals
{
    public class Department
    {
        [JsonConverter(typeof(ObjectIdConverter))]
        [BsonId]
        public ObjectId _objetId { get; set; }

        public int ID { get; set; }

        public string Name { get; set; }

        //public List<Employee> Employees { get; set; }

        //public Operations Operations { get; set; }

    }
}
