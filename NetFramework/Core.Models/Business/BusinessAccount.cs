using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.Business
{
    public class BusinessAccount
    {
        public int ID { get; set; }
        public Organization Party { get; set; }
        public List<DealAccount> DealAccounts { get; set; }
        public float NetReceivable { get; set; }
        public float NetPayable { get; set; }
    }


}
