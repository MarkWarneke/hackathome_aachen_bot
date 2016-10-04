using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication.Logic;
using WebApplication.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication.Controllers
{
   
    public class DeductionController : Controller
    {
        [Route("api/users/{userId}/[controller]")]
        [HttpGet]
        public IActionResult Get(int userId)
        {
            Deduction result = null;
            User user = new UserManager().findById(userId);
            if (user != null)
            {
                result = user.Deduction;
                return Ok(result);
            }
            return NotFound(userId);

        }

        [Route("api/users/{userId}/[controller]")]
        [HttpPost]
        public IActionResult Post([FromBody] Deduction deduction, int userId)
        {
            User user = new UserManager().findById(userId);
            if (user != null)
            {
                user.Deduction = deduction;
                return Ok(deduction);
            }

            return NotFound(userId);
          }
    }
}
