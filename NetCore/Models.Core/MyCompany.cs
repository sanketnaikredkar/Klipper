using System.Collections.Generic;
using Models.Core.Business;
using Models.Core.Employment;
using Models.Core.Operationals;

namespace Models.Core
{

    public class MyCompany : Organization
    {
        public List<Employee> Employees { get; set; }

        public List<Department> Departments { get; set; }

    }
}

