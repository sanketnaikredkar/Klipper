using ERPCore.Models.Employment;
using ERPCore.Models.HR.Attendance;
using ERPCore.Models.Operationals;
using AttendaceDBAccessApi.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AttendaceDBAccessApi.DataAccess.Implementation
{
    public class HRContext
    {
        private readonly IMongoDatabase _database = null;

        public HRContext(IOptions<DBConnectionSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
            {
                _database = client.GetDatabase(settings.Value.Database);
            }
        }

        public static HRContext Instance { get; private set; } = null;

        public static HRContext GetInstance(IOptions<DBConnectionSettings> settings)
        {
            if (Instance == null)
            {
                var context = new HRContext(settings);
                Instance = context;
            }
            return Instance;
        }

        public IMongoCollection<Employee> Employees => _database.GetCollection<Employee>("Employees");
        public IMongoCollection<AccessEvent> AccessEvents => _database.GetCollection<AccessEvent>("AccessEvents");
        public IMongoCollection<Department> Departments => _database.GetCollection<Department>("Departments");
        public IMongoCollection<AccessPoint> AccessPoints => _database.GetCollection<AccessPoint>("AccessPoints");
    }
}
