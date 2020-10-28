using System.Collections.Generic;
using System.Threading.Tasks;
using App.Models;
using App.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private EmployeesService _service;
        public EmployeesController(EmployeesService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IList<EmployeeDTO>>> GetEmployees()
        {
            return Ok(await _service.FindAllEmployees());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployee(int id)
        {
            EmployeeDTO employee = await _service.FindEmployee(id);
            
            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDTO>> PostEmployee(Employee employee)
        {
            EmployeeDTO postedEmployee = await _service.AddEmployee(employee);

            if (postedEmployee == null)
            {
                BadRequest();
            }

            return postedEmployee;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeDTO>> PutEmployee(int id, Employee employee)
        {
            if (id != employee.EmployeeNo)
            {
                return BadRequest();
            }

            EmployeeDTO updatedEmployee = await _service.UpdateEmployee(employee);

            if (updatedEmployee == null)
            {
                return NotFound();
            }

            return updatedEmployee;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<EmployeeDTO>> DeleteEmployee(int id)
        {
            EmployeeDTO deletedEmployee = await _service.RemoveEmployee(id);

            if (deletedEmployee == null)
            {
                return NotFound();
            }

            return deletedEmployee;
        }
    }
}
