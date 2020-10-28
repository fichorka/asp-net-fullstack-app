using App.Data;
using App.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services
{
    public class DepartmentsService
    {
        private CompanyContext _context;

        public DepartmentsService(CompanyContext context)
        {
            _context = context;
        }

        public async Task<IList<DepartmentDTO>> FindAllDepartments()
        {
            return await _context.Department
                .Select(dep => DepartmentToDTO(dep)).ToListAsync();
        }

        public async Task<DepartmentDTO> FindDepartment(int id)
        {
            return DepartmentToDTO(await _context.Department.FindAsync(id));
        }

        public async Task<DepartmentDTO> AddDepartment(Department department)
        {
            _context.Department.Add(department);
            await _context.SaveChangesAsync();

            return DepartmentToDTO(department);
        }

        public async Task<DepartmentDTO> UpdateDepartment(Department newDepartment)
        {
            var department = await _context.Department.FindAsync(newDepartment.DepartmentNo);

            if (department == null)
            {
                return null;
            }

            department.DepartmentName = newDepartment.DepartmentName;
            department.DepartmentLocation = newDepartment.DepartmentLocation;

            await _context.SaveChangesAsync();

            return DepartmentToDTO(department);
        }

        public async Task<DepartmentDTO> RemoveDepartment(int id)
        {
            var department = await _context.Department.FindAsync(id);

            if (department == null)
            {
                return null;
            }

            _context.Department.Remove(department);
            await _context.SaveChangesAsync();

            return DepartmentToDTO(department);
        }

        private static DepartmentDTO DepartmentToDTO(Department department)
        {
            if (department == null)
            {
                return null;
            }

            return new DepartmentDTO
             {
                DepartmentNo = department.DepartmentNo,
                DepartmentLocation = department.DepartmentLocation,
                DepartmentName = department.DepartmentName
             };
        }
    }
}
