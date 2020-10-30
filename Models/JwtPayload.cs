using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace App.Models
{
    public class JwtPayload
    {
        public string SecurityAlgorythm { get; set; } = SecurityAlgorithms.HmacSha256Signature;
        public int ExpireMinutes { get; set; } = 60 ;

        public Claim[] Claims { get; set; }
    }
}
