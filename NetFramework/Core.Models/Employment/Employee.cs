using System;
using System.Collections.Generic;
using Core.Models.Operationals;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Models.Employment
{
    public class Employee : Person
    {
        public string Title { get; set; }

        [BsonDateTimeOptions]
        public DateTime JoiningDate { get; set; }

        public Credential Credentials { get; set; }

        public int BusinessGroup { get; set; }

        public int Department { get; set; }

        public int SubDivision { get; set; }

        public DateTime LastUpdatedOn { get; set; } = DateTime.Now;

        public int ReportingSuperiorID { get; set; } = -1;

        public List<int> Reportees { get; set; } = new List<int>();

    }
}
