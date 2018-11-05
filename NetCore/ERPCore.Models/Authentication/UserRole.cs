using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using ERPCore.Models.Helpers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ERPCore.Models
{
    public class UserRole
    {
        [BsonId]
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId _objetId { get; set; }

        public int ID { get; set; }

        [Required]
        [StringLength(25)]
        [Display(Name = "User Role")]
        public string Role { get; set; }
    }
}
