using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TecMailAPI.Controller
{
    [Route("TecMail/[controller]")]
    
    [ApiController]
    public class TestController : ControllerBase
    {
        // GET: TecMail/api/Test
        [HttpGet]
        public string Get()
        {
            return "Welcome to TecMail Web Api";
        }
    }
}