using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class Employee
    {
        public int EmployeeNo { get; set; }
        public string EmployeeName { get; set; }
        public string Salary { get; set; }
        public DateTime LastModifyDate { get; set; }

        public Department DepartmentNo { get; set; }
    }
}