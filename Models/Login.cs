using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace App.Models
{
    [Table("Login")]
    public class Login
    {
        [Key]
        public int LoginNo { get; set; }
        [MaxLength(20)]
        public string LoginUserName { get; set; }
        [MaxLength(20)]
        public string LoginPassword { get; set; }
    }
}