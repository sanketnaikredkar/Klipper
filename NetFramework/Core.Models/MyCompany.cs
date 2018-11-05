using System.Collections.Generic;
using Core.Models.Business;
using Core.Models.Employment;
using Core.Models.Operationals;

namespace Core.Models
{

    public class MyCompany : Organization
    {
        public List<Employee> Employees { get; set; }

        public Portfolio BusinessPortfolio { get; set; }

        public Finance Financials { get; set; }

        public List<Department> Departments { get; set; }

    }
}

