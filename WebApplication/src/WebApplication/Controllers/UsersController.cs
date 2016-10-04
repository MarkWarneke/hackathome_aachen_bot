using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Logic;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new UserManager().getUser());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            User User = new UserManager().findById(id);
            if (User  != null)
            {
                return Ok(User);
            }
            return NotFound(id);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]User User)
        {
            return Ok(new UserManager().updateUser(User));
        }
    }
}
