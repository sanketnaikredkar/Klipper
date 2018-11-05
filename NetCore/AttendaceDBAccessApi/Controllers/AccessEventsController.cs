using System.Collections.Generic;
using System.Threading.Tasks;
using AttendaceApi.DataAccess.Interfaces;
using ERPCore.Models.Employment;
using ERPCore.Models.HR.Attendance;
using AttendaceApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;

namespace AttendaceApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccessEventsController : ControllerBase
    {
        private readonly IAccessEventRepository _accessEventsRepository = null;

        public AccessEventsController(IAccessEventRepository repository)
        {
            _accessEventsRepository = repository;
        }

        // GET api/values
        [NoCache]
        [HttpGet]
        public Task<IEnumerable<AccessEvent>> Get()
        {
            return GetAccessEvents_Internal();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Task<AccessEvent> Get(int id)
        {
            return GetAccessEventById_Internal(id);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] AccessEvent value)
        {
            _accessEventsRepository.AddAccessEvent(value);
            return CreatedAtAction("Get", new { id = value.ID }, value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] AccessEvent value)
        {
            value.ID = id;
            _accessEventsRepository.UpdateAccessEvent(id, value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _accessEventsRepository.RemoveAccessEvent(id);
        }

        #region Internals

        private async Task<IEnumerable<AccessEvent>> GetAccessEvents_Internal()
        {
            return await _accessEventsRepository.GetAllAccessEvents();
        }

        private async Task<AccessEvent> GetAccessEventById_Internal(int id)
        {
            return await _accessEventsRepository.GetAccessEvent(id) ?? new AccessEvent();
        }

        #endregion

    }
}
