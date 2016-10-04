using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Logic;
using WebApplication.Models;

namespace WebApplication.Controllers
{

    public class ChatMessageController : Controller
    {
        [Route("api/users/{userId}/[controller]")]
        [HttpGet]
        public IActionResult Get(int userId)
        {
            User user = new UserManager().findById(userId);
            if (user != null)
            {
                return Ok(user.ChatMessages);
                
            }
            return NotFound(userId);

        }

        [Route("api/users/{userId}/[controller]")]
        [HttpPost]
        public IActionResult Post([FromBody] Chat Chat, int userId)
        {
            User user = new UserManager().findById(userId);
            if (user != null)
            {
                user.ChatMessages.Add(Chat);
                return Ok(Chat);
            }
            return NotFound(userId);

        }
    }
}
