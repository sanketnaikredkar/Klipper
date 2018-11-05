using AuthServer.DataAccess.Database;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.DataAccess
{
    public class Role
    {
        [BsonId]
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId _objetId { get; set; }

        public string Name { get; set; }

        public List<string> Subjects { get; internal set; } = new List<string>();
    }
}
