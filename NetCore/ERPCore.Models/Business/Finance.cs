using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPCore.Models.Business
{
    public class Finance
    {
        public int ID { get; set; }

        public List<TaxAccount> TaxAccounts { get; set; }
    }
}
