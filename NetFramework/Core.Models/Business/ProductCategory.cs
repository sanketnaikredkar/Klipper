using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.Business
{
    public class ProductCategory
    {
        public int ID { get; set; }

        [StringLength(25)]
        public string CategoryName { get; set; }
    }
}
