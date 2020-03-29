using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlugraryDetectionSystemApi.MiscClasses;
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
            if(reqAddUser != null && ModelState.IsValid)
            {

            }
            else
            {
                return APIResponse.JsonBadRequestResponse(Request, "Invalid parameters passed: " + string.Join(',',ModelState.Values.SelectMany(values => values.Errors).Select(error => error.ErrorMessage)));
            }
        }

    }
}