using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Business
{
    public class Portfolio
    {
        public int ID { get; set; }

        public ProductPortfolio ProductPortfolio { get; set; }

        public SolutionPortfolio SolutionPortfolio { get; set; }

    }


}
