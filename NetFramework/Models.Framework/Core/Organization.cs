using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Framework.Business;
using Models.Framework.Employment;

namespace Models.Framework
{
    public abstract class Organization
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string CompanyName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Registration Date")]
        public DateTime RegistrationDate { get; set; }

        [StringLength(50)]
        [DataType(DataType.Url)]
        public string Website { get; set; }

        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        public string PrimaryEmailAddress { get; set; }

        public byte[] Logo { get; set; }

        public Contact Location { get; set; }

    }

}


