using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class Department
    {
        public int DepartmentNo { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentLocation { get; set; }

        public ICollection<Employee> EmployeeNo { get; set; }
    }
}