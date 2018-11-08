using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Core.Business
{
    [Flags]
    public enum ECompanyType : byte
    {
        Unspecified = 1,
        Self,
        Customer,
        Supplier,
        Partner
    }

    public class Company : Organization
    {
        public ECompanyType PrimaryTrait { get; set; }
        public ECompanyType SecondaryTrait { get; set; }
        public ECompanyType TertiaryTrait { get; set; }

    }
}

