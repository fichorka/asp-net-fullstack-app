using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Api.Models
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        public int EmployeeNo { get; set; }
        [MaxLength(50)]
        [Required]
        public string EmployeeName { get; set; }
        public int Salary { get; set; }
        public DateTime? LastModifyDate { get; set; }

        public int? DepartmentNo { get; set; }
        public Department Department { get; set; }
    }
}