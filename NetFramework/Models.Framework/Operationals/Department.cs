using Models.Framework.Employment;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Models.Framework.Operationals
{
    public class Department
    {
        [BsonId]
        public ObjectId _objetId { get; set; }

        public int ID { get; set; }

        public string Name { get; set; }

        //public List<Employee> Employees { get; set; }

        //public Operations Operations { get; set; }

    }
}
