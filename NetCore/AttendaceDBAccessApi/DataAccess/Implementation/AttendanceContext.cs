using ERPCore.Models.Employment;
using ERPCore.Models.HR.Attendance;
using ERPCore.Models.Operationals;
using AttendaceDBAccessApi.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AttendaceDBAccessApi.DataAccess.Implementation
{
    public class AttendanceContext
    {
        private readonly IMongoDatabase _database = null;

        public AttendanceContext(IOptions<DBConnectionSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
            {
                _database = client.GetDatabase(settings.Value.Database);
            }
        }

        public static AttendanceContext Instance { get; private set; } = null;

        public static AttendanceContext GetInstance(IOptions<DBConnectionSettings> settings)
        {
            if (Instance == null)
            {
                var context = new AttendanceContext(settings);
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
