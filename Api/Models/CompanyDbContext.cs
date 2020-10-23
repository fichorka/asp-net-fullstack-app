using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class CompanyDbContext : DbContext
    {
        public CompanyDbContext() : base("Company")
        {
            Database.SetInitializer(new CompanyDBInitializer());
        }

        public DbSet<Login> Login { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employee { get; set; }
    }

    // Custom DB initializer
    public class CompanyDBInitializer : DropCreateDatabaseIfModelChanges<CompanyDbContext>
    {
        protected override void Seed(CompanyDbContext context)
        {
            IList<Login> defaultLogins = new List<Login>();

            defaultLogins.Add(new Login() { LoginUserName = "Bill", LoginPassword = "ItsNotSoft" });
            defaultLogins.Add(new Login() { LoginUserName = "Jean", LoginPassword = "trollsRule" });

            context.Login.AddRange(defaultLogins);

            IList<Department> defaultDepartments = new List<Department>();

            defaultDepartments.Add(new Department() { DepartmentName = "Development", DepartmentLocation = "Location" });
            defaultDepartments.Add(new Department() { DepartmentName = "Development", DepartmentLocation = "Zurich" });
            defaultDepartments.Add(new Department() { DepartmentName = "Development", DepartmentLocation = "Osijek" });
            defaultDepartments.Add(new Department() { DepartmentName = "Sales", DepartmentLocation = "London" });
            defaultDepartments.Add(new Department() { DepartmentName = "Sales", DepartmentLocation = "Zurich" });
            defaultDepartments.Add(new Department() { DepartmentName = "Sales", DepartmentLocation = "Osijek" });
            defaultDepartments.Add(new Department() { DepartmentName = "Sales", DepartmentLocation = "Basel" });
            defaultDepartments.Add(new Department() { DepartmentName = "Sales", DepartmentLocation = "Lugano" });

            context.Department.AddRange(defaultDepartments);

            IList<Employee> defaultEmployees = new List<Employee>();

            defaultEmployees.Add(new Employee() { EmployeeName = "Fred Davies", Salary = 50000, DepartmentNo = defaultDepartments[3].DepartmentNo });
            defaultEmployees.Add(new Employee() { EmployeeName = "Bernard Katic", Salary = 50000, DepartmentNo = defaultDepartments[2].DepartmentNo });
            defaultEmployees.Add(new Employee() { EmployeeName = "Rich Davies", Salary = 30000, DepartmentNo = defaultDepartments[4].DepartmentNo });
            defaultEmployees.Add(new Employee() { EmployeeName = "Eva Dobos", Salary = 30000, DepartmentNo = defaultDepartments[5].DepartmentNo });
            defaultEmployees.Add(new Employee() { EmployeeName = "Mario Hunjadi", Salary = 25000, DepartmentNo = defaultDepartments[7].DepartmentNo });
            defaultEmployees.Add(new Employee() { EmployeeName = "Jean Michele", Salary = 25000, DepartmentNo = defaultDepartments[6].DepartmentNo });
            defaultEmployees.Add(new Employee() { EmployeeName = "Bill Gates", Salary = 25000, DepartmentNo = defaultDepartments[0].DepartmentNo });
            defaultEmployees.Add(new Employee() { EmployeeName = "Maja Janic", Salary = 30000, DepartmentNo = defaultDepartments[2].DepartmentNo });
            defaultEmployees.Add(new Employee() { EmployeeName = "Igor Horvat", Salary = 35000, DepartmentNo = defaultDepartments[2].DepartmentNo });

            context.Employee.AddRange(defaultEmployees);

            base.Seed(context);
        }
    }
}