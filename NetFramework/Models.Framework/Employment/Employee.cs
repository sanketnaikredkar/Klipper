using System;
using System.Collections.Generic;
using Models.Framework.Authentication;
using Models.Framework.Operationals;
using MongoDB.Bson.Serialization.Attributes;

namespace Models.Framework.Employment
{
    public class Employee : Person
    {
        public string Title { get; set; }

        [BsonDateTimeOptions]
        public DateTime JoiningDate { get; set; }

        public List<UserRole> Roles { get; set; }

        public int BusinessGroup { get; set; }

        public int Department { get; set; }

        public int SubDivision { get; set; }

        public DateTime LastUpdatedOn { get; set; } = DateTime.Now;

        public int ReportingSuperiorID { get; set; } = -1;

        public List<int> Reportees { get; set; } = new List<int>();

    }
}
