using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Framework
{
    public class Contact
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string LocationName { get; set; }

        public Address Address { get; set; }

        [StringLength(15)]
        [DataType(DataType.PhoneNumber)]
        public string PrimaryPhone { get; set; }

        [StringLength(15)]
        [DataType(DataType.PhoneNumber)]
        public string SecondaryPhone { get; set; }

        [StringLength(15)]
        [DataType(DataType.PhoneNumber)]
        public string Fax { get; set; }
    }

}
