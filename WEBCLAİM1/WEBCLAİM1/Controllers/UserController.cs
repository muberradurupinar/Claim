using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEBCLAİM1.Models;

namespace WEBCLAİM1.Controllers
{
    [Route("api/user")]
    [ApiController]
    
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("GetUser")]
        [Authorize(Policy = Policies.User)]
        public IActionResult GetUser()
        {
            return Ok("user girişi");
        }

        [HttpGet]
        [Route("GetAdmin")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult GetAdmin()
        {
            return Ok("admin girişi");
        }
    }
}
