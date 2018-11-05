using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using ERPCore.Models.Helpers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace ERPCore.Models
{
    public abstract class Organization
    {
        [BsonId]
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId _objetId { get; set; }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [BsonDateTimeOptions]
        [DataType(DataType.Date)]
        [Display(Name = "Registration Date")]
        public DateTime RegistrationDate { get; set; }

        [StringLength(50)]
        [DataType(DataType.Url)]
        public string Website { get; set; }

        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        public string PrimaryEmailAddress { get; set; }

        public byte[] Logo { get; set; }

        public List<Contact> Locations { get; set; }

    }

}


