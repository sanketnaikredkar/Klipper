using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using KlipperApi.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Core.HR.Attendance;

namespace KlipperApi.Controllers.Attendance
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmployeeAccessor _employeeAccessor;
        private readonly IAttendanceAccessor _attendanceAccessor;

        public AttendanceController(
            IUserRepository userRepository, 
            IEmployeeAccessor employeeAccessor,
            IAttendanceAccessor attendanceAccessor)
        {
            _userRepository = userRepository;
            _employeeAccessor = employeeAccessor;
            _attendanceAccessor = attendanceAccessor;
        }

        [Route("{employeeId}/{startDate}/{endDate}")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get(int employeeId, string startDate, string endDate)
        {
            var start = DateTime.Parse(startDate);
            var end = DateTime.Parse(endDate);

            var roleClaims = User.Claims.Where(c => c.Type == ClaimTypes.Role).ToList();
            bool isHR = roleClaims.Where(c => c.Value == "HR").First() != null;
            bool isTeamLeader = roleClaims.Where(c => c.Value == "TeamLeader").First() != null;
            bool isEmployee = roleClaims.Where(c => c.Value == "Employee").First() != null;

            if (isHR)
            {
                return await GetAttendance_HR(employeeId, start, end);
            }
            if (isTeamLeader)
            {
                return await GetAttendance_TeamLeader(employeeId, start, end);
            }
            if (isEmployee)
            {
                return await GetAttendance_Employee(employeeId, start, end);
            }
            return NotFound();
        }

        private Task<IActionResult> GetAttendance_Employee(int id, DateTime startdate, DateTime enddate)
        {
            throw new NotImplementedException();
        }

        private Task<IActionResult> GetAttendance_TeamLeader(int id, DateTime startdate, DateTime enddate)
        {
            throw new NotImplementedException();
        }

        private async Task<IActionResult> GetAttendance_HR(int id, DateTime startdate, DateTime enddate)
        {
            var accessEvents = await _attendanceAccessor.GetAttendanceByEmployeeIdAsync(id) as List<AccessEvent>;
            var filteredEvents = accessEvents
                .Where(e => e.EventTime >= startdate && e.EventTime <= enddate)
                .ToList();

            return Ok(filteredEvents);
        }
    }
}
