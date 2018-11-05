using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServer.Configuration;
using AuthServer.DataAccess.Database;
using IdentityServer4.Models;

namespace AuthServer.DataAccess.Stores
{
    public class ClientStore : IdentityServer4.Stores.IClientStore
    {
        List<Client> _clients = new List<Client>();

        public ClientStore()
        {
            _clients = (List<Client>) AuthConfig.GetClients();
        }

        public Task<Client> FindClientByIdAsync(string clientId)
        {
            var client = _clients.Where(c => c.ClientId == clientId).FirstOrDefault();
            return Task.FromResult(client);
        }
    }
}
