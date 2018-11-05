using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPCore.Models.Business
{
    [Flags]
    public enum ETaxAccountType : byte
    {
        Unspecified = 1,
        IncomeTax,
        ServiceTax,
        ValueAddedTax,
        CentralSalesTax,
        CentralExciseTax,
        LocalBodyTax
    }

    public class TaxAccount
    {
        public int ID { get; set; }

        //This will be descriptive name of tax account 
        [StringLength(25)]
        public string Name { get; set; }

        [StringLength(35)]
        public string AccountNo { get; set; }

        public ETaxAccountType AccountType { get; set; }
    }
}
