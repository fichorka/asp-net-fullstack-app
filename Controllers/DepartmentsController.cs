using App.Data;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private CompanyContext _context;

        public DepartmentsController(CompanyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDTO>>> GetDepartments()
        {
            var allDepartments = await _context.Department.Select(dep => DepartmentToDTO(dep)).ToListAsync();

            return allDepartments;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDTO>> GetDepartment(int id)
        {
            var department = await _context.Department.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            return DepartmentToDTO(department);
        }

        [HttpPost]
        public async Task<ActionResult<Department>> CreateDepartment(Department departmentDTO)
        {
            var department = new Department
            {
                DepartmentName = departmentDTO.DepartmentName,
                DepartmentLocation = departmentDTO.DepartmentLocation
            };

            _context.Department.Add(department);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetDepartment),
                new { id = department.DepartmentNo });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, Department departmentDTO)
        {
            if (id != departmentDTO.DepartmentNo)
            {
                return BadRequest();
            }

            var department = await _context.Department.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            department.DepartmentName = departmentDTO.DepartmentName;
            department.DepartmentLocation = departmentDTO.DepartmentLocation;
            
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _context.Department.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            _context.Department.Remove(department);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private static DepartmentDTO DepartmentToDTO(Department department) =>
        new DepartmentDTO
        {
            DepartmentNo = department.DepartmentNo,
            DepartmentName = department.DepartmentName,
            DepartmentLocation = department.DepartmentLocation
        };
    }
}
