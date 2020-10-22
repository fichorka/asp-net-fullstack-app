using System;

namespace Server.Models
{
    public class Employee
    {
        public long Id { get; set; }
        public string EmployeeNAme { get; set; }
        public int Salary { get; set; }
        public long DepartmentId { get; set; }
        public DateTime lastModifyDate { get; set; }
    }
}
