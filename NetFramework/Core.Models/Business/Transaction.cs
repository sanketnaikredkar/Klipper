using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.Business
{
    public class TransactionType
    {
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        public string TypeName { get; set; }
    }

    public class Transaction : UserAction
    {
        public TransactionType Type { get; set; }
        public DealAccount Account { get; set; }
        public bool IsRealized { get; set; }
    }

}
