using Common.DataAccess;
using ERPCore.Models.Employment;
using ERPCore.Models.Operationals;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EmployeeApi.DataAccess.Implementation
{
    public class PeopleDBContext : DBContext
    {
        #region Instance

        public static PeopleDBContext Instance { get; private set; } = null;

        public static PeopleDBContext GetInstance(IOptions<DBConnectionSettings> settings)
        {
            if (Instance == null)
            {
                Instance = new PeopleDBContext(settings);
            }
            return Instance;
        }

        #endregion

        public PeopleDBContext(IOptions<DBConnectionSettings> settings) : base(settings) { }

        public IMongoCollection<Employee> Employees => _database.GetCollection<Employee>("Employees");
        public IMongoCollection<Department> Departments => _database.GetCollection<Department>("Departments");
    }
}
