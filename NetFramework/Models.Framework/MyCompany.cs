using System.Collections.Generic;
using Models.Framework.Business;
using Models.Framework.Employment;
using Models.Framework.Operationals;

namespace Models.Framework
{

    public class MyCompany : Organization
    {
        public List<Employee> Employees { get; set; }

        public List<Department> Departments { get; set; }

    }
}

