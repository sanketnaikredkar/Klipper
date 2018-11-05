using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthServer.DataAccess.Database
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<Permission>> GetAllPermissions();

        Task<Permission> Get(string id);

        Task<bool> Exists(string id);

        Task<bool> Add(Permission item);

        Task<bool> Remove(string id);

        Task<bool> RemoveAll();

        Task<bool> Update(string id, Permission item);
    }
}
