using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Api.Models
{
    [Table("Department")]
    public class Department
    {
        [Key]
        public int DepartmentNo { get; set; }
        [MaxLength(20)]
        public string DepartmentName { get; set; }
        [MaxLength(20)]
        public string DepartmentLocation { get; set; }

        public ICollection<Employee> Employee { get; set; }
    }
}