using System.Collections.Generic;
using System.Threading.Tasks;
using AttendanceApi.DataAccess.Interfaces;
using ERPCore.Models.Employment;
using ERPCore.Models.HR.Attendance;
using AttendanceApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AttendanceApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccessPointsController : ControllerBase
    {
        private readonly IAccessPointRepository _accessPointsRepository = null;

        public AccessPointsController(IAccessPointRepository repository)
        {
            _accessPointsRepository = repository;
        }

        // GET api/values
        [NoCache]
        [HttpGet]
        public Task<IEnumerable<AccessPoint>> Get()
        {
            return GetAccessPoints_Internal();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Task<AccessPoint> Get(int id)
        {
            return GetAccessPointById_Internal(id);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] AccessPoint value)
        {
            _accessPointsRepository.AddAccessPoint(value);
            return CreatedAtAction("Get", new { id = value.ID }, value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] AccessPoint value)
        {
            value.ID = id;
            _accessPointsRepository.UpdateAccessPoint(id, value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _accessPointsRepository.RemoveAccessPoint(id);
        }

        #region Internals

        private async Task<IEnumerable<AccessPoint>> GetAccessPoints_Internal()
        {
            return await _accessPointsRepository.GetAllAccessPoints();
        }

        private async Task<AccessPoint> GetAccessPointById_Internal(int id)
        {
            return await _accessPointsRepository.GetAccessPoint(id) ?? new AccessPoint();
        }

        #endregion

    }
}
