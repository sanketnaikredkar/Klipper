using ERPCore.Models.HR.Attendance;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendaceDBAccessApi.DataAccess.Interfaces
{
    public interface IAccessEventRepository
    {
        Task<IEnumerable<AccessEvent>> GetAllAccessEvents();

        Task<AccessEvent> GetAccessEvent(int id);

        Task AddAccessEvent(AccessEvent item);

        Task<bool> RemoveAccessEvent(int id);

        Task<bool> UpdateAccessEvent(int id, AccessEvent item);

        Task<bool> RemoveAllAccessEvents();
    }
}
