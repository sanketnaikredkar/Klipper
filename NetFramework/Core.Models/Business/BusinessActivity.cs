using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Models.Business;
using Core.Models.Employment;

namespace Core.Models.Business
{
    public class BusinessActivity
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public Organization DealingParty { get; set; }

        public DealAccount ActivityAccount { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        public Employee StartedBy { get; set; }

        public Employee EndedBy { get; set; }

        public List<Transaction> Transactions { get; set; }
    }

    public class Sale : BusinessActivity
    {
    }

    public class Purchase : BusinessActivity
    {

    }

}



