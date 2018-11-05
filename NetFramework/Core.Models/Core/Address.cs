using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class Address
    {
        public int ID { get; set; }

        [StringLength(25)]
        public string Building { get; set; }

        [StringLength(25)]
        public string Street { get; set; }

        [StringLength(50)]
        public string Locality { get; set; }

        [StringLength(25)]
        public string City { get; set; }

        [StringLength(25)]
        public string State { get; set; }

        [StringLength(20)]
        public string Country { get; set; }

        [StringLength(15)]
        [DataType(DataType.PostalCode)]
        public string ZipCode { get; set; }
    }
}
