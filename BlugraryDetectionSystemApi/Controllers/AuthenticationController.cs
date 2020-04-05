using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlugraryDetectionSystemApi.MiscClasses;
using BlugraryDetectionSystemApi.Services.Contracts;
using BlugraryDetectionSystemEntities;
using BlugraryDetectionSystemEntities.RequestEntities;
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
        [HttpPost("Authenticate")]
        public ContentResult Authenticate([FromBody]ReqUserAuth reqUserAuth)
        {
            try
            {
                if (reqUserAuth != null && ModelState.IsValid)
                {
                    ResAuthToken authToken = userAuthenticationService.Authenticate(reqUserAuth);

                    if (authToken == null && !string.IsNullOrEmpty(authToken.Token) && !string.IsNullOrEmpty(authToken.UserName))
                        return APIResponse.JsonUnauthorizedResponse(Request);
                    else
                        return APIResponse.JsonSuccessResponse(Request,authToken);
                }
                else
                {
                    return APIResponse.JsonBadRequestResponse(Request, "Invalid parameters passed: " + string.Join(',', ModelState.Values.SelectMany(values => values.Errors).Select(error => error.ErrorMessage)));
                }
            }
            catch(Exception ex)
            {
                return APIResponse.JsonInternelServerErrorResponse(Request, ex);
            }
        }



    }
}
