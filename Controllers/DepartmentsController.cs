using App.Models;
using App.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private DepartmentsService _service;
        private JwtService _jwtService;

        public DepartmentsController(DepartmentsService service, JwtService jwtService)
        {
            _service = service;
            _jwtService = jwtService;
        }

        [HttpGet]
        public async Task<ActionResult<IList<DepartmentDTO>>> GetDepartments([FromHeader] string Authorization)
        {
            if (string.IsNullOrEmpty(Authorization) || !_jwtService.IsTokenValid(Authorization))
            {
                return Unauthorized();
            }

            return Ok(await _service.FindAllDepartments());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDTO>> GetDepartment(int id, [FromHeader] string Authorization)
        {
            if (string.IsNullOrEmpty(Authorization) || !_jwtService.IsTokenValid(Authorization))
            {
                return Unauthorized();
            }

            DepartmentDTO department = await _service.FindDepartment(id);

            if (department == null)
            {
                return NotFound();
            }

            return department;
        }

        [HttpPost]
        public async Task<ActionResult<Department>> CreateDepartment(Department newDepartment, [FromHeader] string Authorization)
        {
            if (string.IsNullOrEmpty(Authorization) || !_jwtService.IsTokenValid(Authorization))
            {
                return Unauthorized();
            }

            var createdDepartmentDTO = await _service.AddDepartment(newDepartment);

            if (createdDepartmentDTO == null)
            {
                return BadRequest();
            }

            return CreatedAtAction(
                nameof(GetDepartment),
                new { id = createdDepartmentDTO.DepartmentNo },
                createdDepartmentDTO
                );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DepartmentDTO>> UpdateDepartment(int id, Department department, [FromHeader] string Authorization)
        {
            if (string.IsNullOrEmpty(Authorization) || !_jwtService.IsTokenValid(Authorization))
            {
                return Unauthorized();
            }

            if (id != department.DepartmentNo)
            {
                return BadRequest();
            }

            var updatedDepartment = await _service.UpdateDepartment(department);
            if (updatedDepartment == null)
            {
                return NotFound();
            }

            return updatedDepartment;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DepartmentDTO>> DeleteDepartment(int id, [FromHeader] string Authorization)
        {
            if (string.IsNullOrEmpty(Authorization) || !_jwtService.IsTokenValid(Authorization))
            {
                return Unauthorized();
            }

            var deletedDepartment = await _service.RemoveDepartment(id);

            if (deletedDepartment == null)
            {
                return NotFound();
            }

            return deletedDepartment;
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
