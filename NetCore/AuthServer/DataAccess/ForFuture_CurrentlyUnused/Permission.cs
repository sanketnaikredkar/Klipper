using AuthServer.DataAccess.Database;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AuthServer.DataAccess
{
    public class Permission
    {
        [BsonId]
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId _objetId { get; set; }

        public string Name { get; set; }

        public List<string> Roles { get; internal set; } = new List<string>();
    }
}
