using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class Login
    {
        public int LoginNo { get; set; }
        public string LoginUserName { get; set; }
        public string LoginPassword { get; set; }
    }
}