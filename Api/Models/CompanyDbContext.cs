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

            Department dep1 = new Department() { DepartmentName = "Development", DepartmentLocation = "Location" };
            Department dep2 = new Department() { DepartmentName = "Development", DepartmentLocation = "Zurich" };
            Department dep3 = new Department() { DepartmentName = "Development", DepartmentLocation = "Osijek" };
            Department dep4 = new Department() { DepartmentName = "Sales", DepartmentLocation = "London" };
            Department dep5 = new Department() { DepartmentName = "Sales", DepartmentLocation = "Zurich" };
            Department dep6 = new Department() { DepartmentName = "Sales", DepartmentLocation = "Osijek" };
            Department dep7 = new Department() { DepartmentName = "Sales", DepartmentLocation = "Basel" };
            Department dep8 = new Department() { DepartmentName = "Sales", DepartmentLocation = "Lugano" };

            context.Department.AddRange(defaultDepartments);

            IList<Employee> defaultEmployees = new List<Employee>();

            Employee emp1 = new Employee() { EmployeeName = "Fred Davies", Salary = 50000, DepartmentNo = dep4.DepartmentNo };
            Employee emp2 = new Employee() { EmployeeName = "Bernard Katic", Salary = 50000, DepartmentNo = dep3.DepartmentNo };
            Employee emp3 = new Employee() { EmployeeName = "Rich Davies", Salary = 30000, DepartmentNo = dep5.DepartmentNo };
            Employee emp4 = new Employee() { EmployeeName = "Eva Dobos", Salary = 30000, DepartmentNo = dep6.DepartmentNo };
            Employee emp5 = new Employee() { EmployeeName = "Mario Hunjadi", Salary = 25000, DepartmentNo = dep8.DepartmentNo };
            Employee emp6 = new Employee() { EmployeeName = "Jean Michele", Salary = 25000, DepartmentNo = dep7.DepartmentNo };
            Employee emp7 = new Employee() { EmployeeName = "Bill Gates", Salary = 25000, DepartmentNo = dep1.DepartmentNo };
            Employee emp8 = new Employee() { EmployeeName = "Maja Janic", Salary = 30000, DepartmentNo = dep3.DepartmentNo };
            Employee emp9 = new Employee() { EmployeeName = "Igor Horvat", Salary = 35000, DepartmentNo = dep3.DepartmentNo };

            context.Employee.AddRange(defaultEmployees);

            base.Seed(context);
        }
    }
}