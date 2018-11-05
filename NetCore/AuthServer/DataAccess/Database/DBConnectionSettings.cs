using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.DataAccess.Database
{
    public class DBConnectionSettings
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
    }
}
