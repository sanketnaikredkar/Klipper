using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPCore.Models.Business
{
    public class SolutionPortfolio
    {
        public int ID { get; set; }

        public List<Solution> Solutions { get; set; }

        public List<SolutionCategory> SolutionCategories { get; set; }

    }
}
