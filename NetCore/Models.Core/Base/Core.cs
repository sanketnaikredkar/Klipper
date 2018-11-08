using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Models.Core.Helpers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models.Core
{
    public class UserAction
    {
        [BsonId]
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId _objetId { get; set; }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        [BsonDateTimeOptions]
        public DateTime Date { get; set; }
    }

}
