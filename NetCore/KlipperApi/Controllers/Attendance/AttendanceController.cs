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
//using PolicyServer.Runtime.Client;

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

        //// GET: api/Attendance
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Attendance/5
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Get([FromBody] dynamic data)
        {
            int id = data.id;
            DateTime startdate = data.startdate;
            DateTime enddate = data.enddate;

            var roleClaims = User.Claims.Where(c => c.Type == ClaimTypes.Role).ToList();
            bool isHR = roleClaims.Where(c => c.Value == "HR").First() != null;
            bool isTeamLeader = roleClaims.Where(c => c.Value == "TeamLeader").First() != null;
            bool isEmployee = roleClaims.Where(c => c.Value == "Employee").First() != null;

            if (isHR)
            {
                return await GetAttendance_HR(id, startdate, enddate);
            }
            if (isTeamLeader)
            {
                return await GetAttendance_TeamLeader(id, startdate, enddate);
            }
            if (isEmployee)
            {
                return await GetAttendance_Employee(id, startdate, enddate);
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

            return Ok(new
            {
                events = filteredEvents
            });
        }
    }
}
