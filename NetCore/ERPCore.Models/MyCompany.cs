using System.Collections.Generic;
using ERPCore.Models.Business;
using ERPCore.Models.Employment;
using ERPCore.Models.Operationals;

namespace ERPCore.Models
{

    public class MyCompany : Organization
    {
        public List<Employee> Employees { get; set; }

        public List<Department> Departments { get; set; }

    }
}

