using App.Data;
using App.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services
{
    public class EmployeesService
    {

        CompanyContext _context;

        public EmployeesService(CompanyContext context)
        {
            _context = context;
        }

        public async Task<IList<EmployeeDTO>> FindAllEmployees()
        {
            return await _context.Employee
                .Select(emp => EmployeeToDTO(emp))
                .ToListAsync();
        }

        public async Task<EmployeeDTO> FindEmployee(int id)
        {
            return EmployeeToDTO(await _context.Employee.FindAsync(id));
        }

        public async Task<EmployeeDTO> AddEmployee(Employee employee)
        {
            if (employee.DepartmentNo != null && !DepartmentExists(employee.DepartmentNo))
            {
                return null;
            }

            employee.LastModifyDate = DateTime.Now;
            _context.Employee.Add(employee);
            await _context.SaveChangesAsync();

            return EmployeeToDTO(employee);
        }

        public async Task<EmployeeDTO> UpdateEmployee(Employee newEmployee)
        {
            Employee existingEmployee = await _context.Employee.FindAsync(newEmployee.EmployeeNo);

            if (existingEmployee == null)
            {
                return null;
            }

            existingEmployee.EmployeeName = newEmployee.EmployeeName;
            existingEmployee.Salary = newEmployee.Salary;
            existingEmployee.DepartmentNo = newEmployee.DepartmentNo;
            existingEmployee.LastModifyDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return EmployeeToDTO(existingEmployee);
        }

        public async Task<EmployeeDTO> RemoveEmployee(int id)
        {
            Employee employee = await _context.Employee.FindAsync(id);

            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();

            return EmployeeToDTO(employee);
        }

        private static EmployeeDTO EmployeeToDTO(Employee employee)
        {
            if (employee == null)
            {
                return null;
            }

            return new EmployeeDTO
            {
                EmployeeNo = employee.EmployeeNo,
                EmployeeName = employee.EmployeeName,
                Salary = employee.Salary,
                DepartmentNo = employee.DepartmentNo
            };
        }

        private bool DepartmentExists(int? id) =>
            _context.Department.Any(dep => id == dep.DepartmentNo);
    }
}
