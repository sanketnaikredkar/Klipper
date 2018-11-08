using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Newtonsoft.Json;
using Models.Core.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Models.Core
{
    public enum Gender
    {
        Male,
        Female
    }

    public class Person
    {
        [BsonId]
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId _objetId { get; set; }

        public int ID { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public Gender Gender { get; set; }

        [StringLength(10)]
        public string Prefix { get; set; }

        [BsonDateTimeOptions]
        public DateTime RegistrationDate { get; set; }

        [BsonDateTimeOptions]
        public DateTime BirthDate { get; set; }

        public Address HomeAddress { get; set; }
        public Address WorkAddress { get; set; }

        [StringLength(15)]
        [DataType(DataType.PhoneNumber)]
        public string MobilePhone { get; set; }

        [StringLength(15)]
        [DataType(DataType.PhoneNumber)]
        public string WorkPhone { get; set; }

        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public byte[] Photo { get; set; }

        //public List<SocialProfile> SocialConnections { get; set; }
    }

}
