using AuthServer.DataAccess.Database;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AuthServer.DataAccess
{
    public class Policy
    {
        [BsonId]
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId _objetId { get; set; }

        public List<Role> Roles { get; internal set; } = new List<Role>();
        public List<Permission> Permissions { get; internal set; } = new List<Permission>();
    }
}
