using App.Data;
using App.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services
{
    public class AppAuthentication { 

        CompanyContext _context;

        public AppAuthentication(CompanyContext context)
        {
            _context = context;
        }

        public async Task<bool> IsAuthentic(Login loginInfo)
        {
            if (string.IsNullOrEmpty(loginInfo.LoginUserName) || string.IsNullOrEmpty(loginInfo.LoginPassword))
            {
                return false;
            }

            IList<Login> logins = await _context.Login.Where(login => login.LoginUserName == loginInfo.LoginUserName).ToListAsync();

            if (logins.Count > 0)
            {
                if (logins[0].LoginPassword == loginInfo.LoginPassword)
                {
                    return true;
                }
                return false;
            }

            return false;
        }
    }
}
