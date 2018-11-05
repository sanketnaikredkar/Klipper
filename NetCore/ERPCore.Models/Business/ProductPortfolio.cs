using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPCore.Models.Business
{
    public class ProductPortfolio
    {
        public int ID { get; set; }

        public List<Product> Products { get; set; }

        public List<ProductCategory> ProductCategories { get; set; }

    }
}
