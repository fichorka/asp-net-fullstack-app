using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace App.Models
{
    [Table("Department")]
    public class Department
    {
        [Key]
        public int DepartmentNo { get; set; }
        [MaxLength(20)]
        [Required]
        public string DepartmentName { get; set; }
        [MaxLength(20)]
        [Required]
        public string DepartmentLocation { get; set; }

        public ICollection<Employee> Employee { get; set; }
    }

    public class DepartmentDTO
    {
        public int DepartmentNo { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentLocation { get; set; }
    }
}