using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthServer.DataAccess.Database
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllRoles();

        Task<Role> Get(string id);

        Task<bool> Exists(string id);

        Task<bool> Add(Role item);

        Task<bool> Remove(string id);

        Task<bool> RemoveAll();

        Task<bool> Update(string id, Role item);
    }
}
