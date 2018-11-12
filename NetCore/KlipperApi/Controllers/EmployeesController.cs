using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Common.Infrastructure;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Core.HR.Attendance;
using Newtonsoft.Json;

namespace KlipperApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        static HttpClient _client = new HttpClient() { Timeout = TimeSpan.FromMilliseconds(10000) };

        public EmployeesController()
        {
            _client.BaseAddress = new Uri("https://localhost:6001/");

            var token = GetTokenAsync().Result; //Get and set Authentication bearer token
            _client.SetBearerToken(token);

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

        }

        #region HTTP methods

        // GET api/AccessEvents
        [NoCache]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
            //return Ok(_repository.GetAllAccessEvents().Result);
        }

        // GET api/AccessEvents/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            //if (id < 0)
            //{
            //    return BadRequest();
            //}
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            //if (!_repository.Exists(id).Result)
            //{
            //    return NotFound();
            //}
            //var value = _repository.Get(id).Result;
            //if (value == null)
            //{
            //    return NotFound();
            //}
            //return Ok(value);

            return NotFound();

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
            //if (!_repository.Add(value).Result)
            //{
            //    return StatusCode(500, "A problem while handling your request!");
            //}
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
            //if (!_repository.Exists(id).Result)
            //{
            //    return NotFound();
            //}
            //value.ID = id;
            //if (_repository.Update(id, value).Result)
            //{
            //    return StatusCode(202, "Updated Successfully!");
            //}
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
            //if (!_repository.Exists(id).Result)
            //{
            //    return NotFound();
            //}
            //if (_repository.Remove(id).Result)
            //{
            //    return Ok();
            //}
            return NoContent();
        }

        #endregion

        #region Private methods

        private static async Task<string> GetTokenAsync()
        {
            var discoveryResponse = await DiscoveryClient.GetAsync("https://localhost:49333/");
            if (discoveryResponse.IsError)
            {
                Console.WriteLine("Error pushing attendance data to AttendanceApi: {0}", discoveryResponse.Error);
                return null;
            }

            var tokenClient = new TokenClient(discoveryResponse.TokenEndpoint, "AttendancePushApp", "Adpa@98765");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("AttendanceApi");
            if (tokenResponse.IsError)
            {
                Console.WriteLine("Error pushing attendance data to AttendanceApi -> Token end point error: {0}", tokenResponse.Error);
                return null;
            }
            //Console.WriteLine(tokenResponse.Json);
            //Console.WriteLine("\n\n");

            return tokenResponse.AccessToken;
        }
        #endregion

    }
}
