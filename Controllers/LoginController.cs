using App.Data;
using App.Models;
using App.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private AppAuthentication _authenticationService;
        private JwtService _jwtService;

        public LoginController(AppAuthentication service, CompanyContext context, JwtService jwtService)
        {
            _authenticationService = service;
            _jwtService = jwtService;
        }

        [HttpPost]
        public async Task<ActionResult> PostLogin(Login loginInfo)
        {
            if (await _authenticationService.IsAuthentic(loginInfo))
            {
                return Ok(new
                { token =
                    _jwtService.GenerateToken(new JwtPayload
                    {
                        Claims = new Claim[]
                        {
                            new Claim("loginUserName", loginInfo.LoginUserName)
                        }
                    })
                });
            }
            return BadRequest();
        }
    }
}
