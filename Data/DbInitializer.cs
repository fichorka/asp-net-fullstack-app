using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Data
{
    public class DbInitializer
    {
        public static void Initialize(CompanyContext context)
        {
            context.Database.EnsureCreated();

            // Look for any department
            if (context.Department.Any())
            {
                return;   // DB has been seeded
            }

            var logins = new Login[]
            {
                new Login() { LoginUserName = "Bill", LoginPassword = "ItsNotSoft" },
                new Login() { LoginUserName = "Jean", LoginPassword = "trollsRule" }
            };

            context.Login.AddRange(logins);
            context.SaveChanges();

            var departments = new Department[]
            {
                new Department() { DepartmentName = "Development", DepartmentLocation = "Location" },
                new Department() { DepartmentName = "Development", DepartmentLocation = "Zurich" },
                new Department() { DepartmentName = "Development", DepartmentLocation = "Osijek" },
                new Department() { DepartmentName = "Sales", DepartmentLocation = "London" },
                new Department() { DepartmentName = "Sales", DepartmentLocation = "Zurich" },
                new Department() { DepartmentName = "Sales", DepartmentLocation = "Osijek" },
                new Department() { DepartmentName = "Sales", DepartmentLocation = "Basel" },
                new Department() { DepartmentName = "Sales", DepartmentLocation = "Lugano" }
            };
            
            context.Department.AddRange(departments);
            context.SaveChanges();

            var employees = new Employee[]
            {
                new Employee() { EmployeeName = "Fred Davies", Salary = 50000, DepartmentNo = departments[3].DepartmentNo },
                new Employee() { EmployeeName = "Bernard Katic", Salary = 50000, DepartmentNo = departments[2].DepartmentNo },
                new Employee() { EmployeeName = "Rich Davies", Salary = 30000, DepartmentNo = departments[4].DepartmentNo },
                new Employee() { EmployeeName = "Eva Dobos", Salary = 30000, DepartmentNo = departments[5].DepartmentNo },
                new Employee() { EmployeeName = "Mario Hunjadi", Salary = 25000, DepartmentNo = departments[7].DepartmentNo },
                new Employee() { EmployeeName = "Jean Michele", Salary = 25000, DepartmentNo = departments[6].DepartmentNo },
                new Employee() { EmployeeName = "Bill Gates", Salary = 25000, DepartmentNo = departments[0].DepartmentNo },
                new Employee() { EmployeeName = "Maja Janic", Salary = 30000, DepartmentNo = departments[2].DepartmentNo },
                new Employee() { EmployeeName = "Igor Horvat", Salary = 35000, DepartmentNo = departments[2].DepartmentNo }
            };

            context.Employee.AddRange(employees);
            context.SaveChanges();


        }
    }
}
