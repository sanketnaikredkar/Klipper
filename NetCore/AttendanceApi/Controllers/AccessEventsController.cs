using System.Collections.Generic;
using System.Threading.Tasks;
using AttendanceApi.DataAccess.Interfaces;
using Common.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Core.HR.Attendance;
using Newtonsoft.Json;

namespace AttendanceApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccessEventsController : ControllerBase
    {
        private readonly IAccessEventRepository _repository = null;

        public AccessEventsController(IAccessEventRepository repository)
        {
            _repository = repository;
        }

        #region HTTP methods

        // GET api/AccessEvents
        [NoCache]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.GetAllAccessEvents().Result);
        }

        // GET api/AccessEvents/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_repository.Exists(id).Result)
            {
                return NotFound();
            }
            var value = _repository.Get(id).Result;
            if (value == null)
            {
                return NotFound();
            }
            return Ok(value);
        }

        // POST api/AccessEvents
        [HttpPost]
        public IActionResult Post([FromBody] AccessEvent value)
        {
            if (value == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_repository.Add(value).Result)
            {
                return StatusCode(500, "A problem while handling your request!");
            }
            return CreatedAtAction("Get", new { id = value.ID }, value);
        }

        // PUT api/AccessEvents/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] AccessEvent value)
        {
            if (value == null || id < 0)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_repository.Exists(id).Result)
            {
                return NotFound();
            }
            value.ID = id;
            if (_repository.Update(id, value).Result)
            {
                return StatusCode(202, "Updated Successfully!");
            }
            return NoContent();
        }

        // DELETE api/AccessEvents/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_repository.Exists(id).Result)
            {
                return NotFound();
            }
            if (_repository.Remove(id).Result)
            {
                return Ok();
            }
            return NoContent();
        }

        #endregion

    }
}
