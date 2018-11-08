using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AuthServer.DataAccess.Database
{
    public class AuthContext
    {
        private readonly IMongoDatabase _database = null;

        public AuthContext(IOptions<DBConnectionSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
            {
                _database = client.GetDatabase(settings.Value.Database);
            }
        }

        public static AuthContext Instance { get; private set; } = null;

        public static AuthContext GetInstance(IOptions<DBConnectionSettings> settings)
        {
            if (Instance == null)
            {
                var context = new AuthContext(settings);
                Instance = context;
            }
            return Instance;
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");

        //Currently unused collections
        //public IMongoCollection<Role> Roles => _database.GetCollection<Role>("Roles");
        //public IMongoCollection<Permission> Permissions => _database.GetCollection<Permission>("Permissions");
    }
}
