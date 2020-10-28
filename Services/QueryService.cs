﻿using App.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services
{
    public class QueryService
    {
        private CompanyContext _context;
        public QueryService(CompanyContext context)
        {
            _context = context;
        }


        public async Task<double> Query1()
        {
            var employees = await _context.Employee.ToListAsync();

            var departments = await _context.Department.ToListAsync();

            var avgSalary = departments.Where(d => d.DepartmentName == "Development" && d.DepartmentLocation != "London")
                .Join(employees, d => d.DepartmentNo, e => e.DepartmentNo, (d, e) => e.Salary).Average();

            return avgSalary;
        }

        public async Task<IEnumerable<Query2ToDTO>> Query2()
        {
            var employees = await _context.Employee.ToListAsync();

            var departments = await _context.Department.ToListAsync();

            var result = departments.GroupJoin(employees, d => d.DepartmentNo, e => e.DepartmentNo, (d, e) => new Query2ToDTO { DepartmentLocation = d.DepartmentLocation, Count = e.Count() });

            foreach (var item in result)
            {
                Console.WriteLine(item.Count);
            }

                return result;
        }

        public async Task<IList<Query3DTO>> Query3()
        {
            var employees = await _context.Employee.ToListAsync();

            var departments = await _context.Department.ToListAsync();

            var result = departments.GroupJoin(employees, d => d.DepartmentNo, e => e.DepartmentNo, (d, matcher) => new Query3DTO {
                DepartmentLocation = d.DepartmentLocation,
                EmployeeCount = matcher.Count((m) => d.DepartmentName == "Development")
            })
                .ToList();

            return result;
        }

        public async Task<int> Query4()
        {
            var employees = await _context.Employee.ToListAsync();

            var result = employees.OrderByDescending(emp => emp.Salary).ToList()[1].Salary;

            return result;
        }
    }

    public class Query2ToDTO
    {
        public string DepartmentLocation { get; set; }
        public int Count { get; set; }
    }

    public class Query3DTO
    {
        public string DepartmentLocation { get; set; }
        public int EmployeeCount { get; set; }
    }
}
