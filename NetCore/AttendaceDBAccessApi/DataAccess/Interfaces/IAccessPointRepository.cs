using ERPCore.Models.HR.Attendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceApi.DataAccess.Interfaces
{
    public interface IAccessPointRepository
    {
        Task<IEnumerable<AccessPoint>> GetAllAccessPoints();

        Task<AccessPoint> GetAccessPoint(int id);

        Task AddAccessPoint(AccessPoint item);

        Task<bool> RemoveAccessPoint(int id);

        Task<bool> RemoveAllAccessPoints();

        Task<bool> UpdateAccessPoint(int id, AccessPoint item);

    }
}
