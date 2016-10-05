using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Logic;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class KontaktController : Controller
    {
        [Route("api/users/{userId}/[controller]")]
        [HttpGet]
        public IActionResult Get( int userId)
        {
            User user = new UserManager().findById(userId);
            if (user != null)
                return Ok(user.Kontakt);
            

            return NotFound(userId);
        }

        [Route("api/users/{userId}/[controller]")]
        [HttpPost]
        public IActionResult Post([FromBody]Kontakt kontakt, int userId)
        {
            User user = new UserManager().findById(userId);
            if (user != null)
            {
                user.Kontakt = kontakt;
                return Ok(kontakt);
            }

            return NotFound(userId);
        }
    }
}
