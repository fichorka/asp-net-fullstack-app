using App.Data;
using App.Models;
using App.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QueryController: Controller
    {
        private CompanyContext _context;
        private QueryService _service;

        public QueryController(CompanyContext context, QueryService service)
        {
            _context = context;
            _service = service;
        }

        [HttpGet("q1")]
        public async Task<ActionResult> Q1()
        {
            return Json(new { avgDeveloperSalaryExcludingLondon = await _service.Query1()});
        }

        [HttpGet("q2")]
        public async Task<IActionResult> Q2()
        {
            return Json(await _service.Query2());
        }

        [HttpGet("q3")]
        public async Task<IActionResult> Q3()
        {
            return Json(await _service.Query3());
        }

        [HttpGet("q4")]
        public async Task<IActionResult> Q4()
        {
            return Json(new { secondHighestSalary = await _service.Query4() });
        }
    }
}
