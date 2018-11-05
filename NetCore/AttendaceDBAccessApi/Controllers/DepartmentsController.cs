using System.Collections.Generic;
using System.Threading.Tasks;
using AttendanceApi.DataAccess.Interfaces;
using ERPCore.Models.Operationals;
using AttendanceApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AttendanceApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentsRepository = null;

        public DepartmentsController(IDepartmentRepository repository)
        {
            _departmentsRepository = repository;
        }

        // GET api/values
        [NoCache]
        [HttpGet]
        public Task<IEnumerable<Department>> Get()
        {
            return GetDepartments_Internal();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Task<Department> Get(int id)
        {
            return GetDepartmentById_Internal(id);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Department value)
        {
            _departmentsRepository.AddDepartment(value);
            return CreatedAtAction("Get", new { id = value.ID }, value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Department value)
        {
            value.ID = id;
            _departmentsRepository.UpdateDepartment(id, value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _departmentsRepository.RemoveDepartment(id);
        }

        #region Internals

        private async Task<IEnumerable<Department>> GetDepartments_Internal()
        {
            return await _departmentsRepository.GetAllDepartments();
        }

        private async Task<Department> GetDepartmentById_Internal(int id)
        {
            return await _departmentsRepository.GetDepartment(id) ?? new Department();
        }

        #endregion

    }
}
