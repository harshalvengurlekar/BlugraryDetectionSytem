using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlugraryDetectionSystemApi.Services.Contracts;
using BlugraryDetectionSystemEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlugraryDetectionSystemApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private IUserAuthenticationService userAuthenticationService;

        public AuthenticationController(IUserAuthenticationService _userAuthenticationService)
        {
            userAuthenticationService = _userAuthenticationService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate()
        {
            User userParam = new User();
            var user = userAuthenticationService.Authenticate(userParam.userName, userParam.password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

       

    }
}
