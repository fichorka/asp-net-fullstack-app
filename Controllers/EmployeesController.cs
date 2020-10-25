using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data;
using App.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private CompanyContext _context;
        public EmployeesController(CompanyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployees()
        {
            return await _context.Employee.Select(emp => EmployeeToDTO(emp)).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployee(int id)
        {
            var employee = await _context.Employee.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return EmployeeToDTO(employee);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDTO>> PostEmployee(Employee employee)
        {
            if (employee.DepartmentNo.HasValue && !DepartmentExists(employee.DepartmentNo)) {
                return BadRequest();
            }

            employee.LastModifyDate = DateTime.Now;

            _context.Employee.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetEmployee),
                new { id = employee.EmployeeNo });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee newEmployee)
        {
            if (id != newEmployee.EmployeeNo)
            {
                return BadRequest();
            }

            var employee = await _context.Employee.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            if (newEmployee.DepartmentNo.HasValue && !DepartmentExists(newEmployee.DepartmentNo))
            {
                return BadRequest();
            }

            employee.EmployeeName = newEmployee.EmployeeName;
            employee.Salary = newEmployee.Salary;
            employee.DepartmentNo = newEmployee.DepartmentNo;
            employee.LastModifyDate = DateTime.Now;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employee.FindAsync(id);

            if(employee == null)
            {
                return NotFound();
            }

            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool DepartmentExists(int? id) =>
            _context.Department.Any(dep => id == dep.DepartmentNo);


        private static  EmployeeDTO EmployeeToDTO(Employee employee) =>
            new EmployeeDTO
            {
                EmployeeNo = employee.EmployeeNo,
                EmployeeName = employee.EmployeeName,
                Salary = employee.Salary,
                DepartmentNo = employee.DepartmentNo
            };
    }
}
