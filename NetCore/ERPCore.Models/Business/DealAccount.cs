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
    public enum ECurrencyType : byte
    {
        Unspecified = 1,
        IndianRupee,
        USDollar,
        Euro,
        BritishPound,
        AustralianDollar,
        JapaneseYen
    }

    public class DealAccount
    {
        public int ID { get; set; }
        public BusinessAccount ParentAccount { get; set; }
        public ECurrencyType CurrencyType { get; set; }
        public List<Transaction> Transactions { get; set; }
        public float TotalReceivable { get; set; }
        public float TotalPayable { get; set; }
    }


}
