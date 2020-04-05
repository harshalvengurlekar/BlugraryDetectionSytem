using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlugraryDetectionSystemApi.MiscClasses;
using BlugraryDetectionSystemBAL.Contracts;
using BlugraryDetectionSystemBAL.Factory;
using BlugraryDetectionSystemEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BlugraryDetectionSystemApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private IUserBAL userBAL;
        private AppSettings appSettings;
        public UserController(IOptionsSnapshot<AppSettings> appsettingsOptions)
        {
            this.appSettings = appsettingsOptions.Value;
            this.userBAL = BALFactory.GetUserBALObj(appSettings);
        }

        [HttpPost("AddUser")]
        public ContentResult AddUser([FromBody]ReqAddUser reqAddUser)
        {
            string response;
            try
            {
                if (reqAddUser != null && ModelState.IsValid)
                {
                    response = userBAL.AddUser(reqAddUser);
                    if (!string.IsNullOrWhiteSpace(response))
                        return APIResponse.JsonSuccessResponse(Request, response);
                    else
                        return APIResponse.JsonInternelServerErrorResponse(Request, new Exception("Something went wrong"));
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