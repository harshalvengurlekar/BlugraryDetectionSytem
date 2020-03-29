using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlugraryDetectionSystemEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlugraryDetectionSystemApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        public UserController()
        {

        }
        
        [HttpPost("adduser")]
        public ContentResult AddUser(ReqAddUser reqAddUser)
        {

        }

    }
}