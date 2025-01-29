using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}