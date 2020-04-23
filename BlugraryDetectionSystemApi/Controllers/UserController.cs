using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlugraryDetectionSystemApi.MiscClasses;
using BlugraryDetectionSystemBAL.Contracts;
using BlugraryDetectionSystemBAL.Factory;
using BlugraryDetectionSystemEntities;
using BlugraryDetectionSystemEntities.RequestEntities;
using BlugraryDetectionSystemEntities.ResponseEntities;
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

        [Authorize(Roles = Role.All)]
        [HttpPost("GetLoggedInUserInfo")]
        public ContentResult GetLoggedInUserInfo([FromBody]ReqGetLoggedUserInfo reqGetLoggedUserInfo)
        {
            ResGetLoggedInUserInfo resGetLoggedInUserInfo;
            try
            {
                resGetLoggedInUserInfo = userBAL.GetLoggedInUserInfo(reqGetLoggedUserInfo);
                if (resGetLoggedInUserInfo != null)
                {
                    return APIResponse.JsonSuccessResponse(Request, resGetLoggedInUserInfo);
                }
                else
                {
                    return APIResponse.JsonNotFoundResponse(Request, "User not found");
                }
            }
            catch (Exception ex)
            {
                return APIResponse.JsonInternelServerErrorResponse(Request, ex);
            }

        }


        [Authorize(Roles = Role.All)]
        [HttpPost("UpdateLoggedInUserInfo")]
        public ContentResult UpdateLoggedInUserInfo([FromBody]ReqUpdateLoggedInUserInfo reqUpdateLoggedInUserInfo)
        {
            string response;
            try
            {
                response = userBAL.UpdateLoggedInUserInfo(reqUpdateLoggedInUserInfo);
                if (!string.IsNullOrEmpty(response))
                {
                    if (response.Contains("No such user"))
                        return APIResponse.JsonNotFoundResponse(Request, response);
                    else
                        return APIResponse.JsonSuccessResponse(Request, response);

                }
                else
                {
                    return APIResponse.JsonInternelServerErrorResponse(Request, new Exception("Something went wrong"));
                }
            }
            catch (Exception ex)
            {
                return APIResponse.JsonInternelServerErrorResponse(Request, ex);
            }

        }


        [HttpPost("UserNameAvailability")]
        public ContentResult CheckUserNameAvailability([FromBody]ReqUserNameAvailability reqUserNameAvailability)
        {
            bool isavailable;
            try
            {
                if (reqUserNameAvailability != null && ModelState.IsValid)
                {
                    isavailable = userBAL.UserNameAvailability(reqUserNameAvailability);
                    return APIResponse.JsonSuccessResponse(Request, Convert.ToString(isavailable));

                }
                else
                {
                    return APIResponse.JsonBadRequestResponse(Request, "Invalid parameters passed: " + string.Join(',', ModelState.Values.SelectMany(values => values.Errors).Select(error => error.ErrorMessage)));
                }
            }
            catch (Exception ex)
            {
                return APIResponse.JsonInternelServerErrorResponse(Request, ex);


            }
        }

        [HttpPost("RegisterUser")]
        public ContentResult AddUser([FromBody]ReqRegisterUser reqRegisterUser)
        {
            string response;
            try
            {
                if (reqRegisterUser != null && ModelState.IsValid)
                {
                    response = userBAL.AddUser(reqRegisterUser);
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
            catch (Exception ex)
            {
                return APIResponse.JsonInternelServerErrorResponse(Request, ex);


            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("GetAllUsers")]
        public ContentResult GetAllUsers()
        {
            List<ResAllUsers> users;
            try
            {
                users = userBAL.GetAllUsers();
                if (users != null && users.Count > 0)
                    return APIResponse.JsonSuccessResponse(Request, users);
                else
                    return APIResponse.JsonNotFoundResponse(Request, "No users found");

            }
            catch (Exception ex)
            {
                return APIResponse.JsonInternelServerErrorResponse(Request, ex);


            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("DeleteUser")]
        public ContentResult DeleteUser([FromBody] ReqDeleteUser reqDeleteUser)
        {
            string response;
            try
            {
                response = userBAL.DeleteUser(reqDeleteUser);
                if (!string.IsNullOrEmpty(response))
                {
                    if (response.Contains("doesn't"))
                        return APIResponse.JsonNotFoundResponse(Request, response);
                    else
                        return APIResponse.JsonSuccessResponse(Request, response);
                }
                else
                {
                    return APIResponse.JsonNotFoundResponse(Request, response);
                }

            }
            catch (Exception ex)
            {
                return APIResponse.JsonInternelServerErrorResponse(Request, ex);
            }
        }

        [Authorize]
        [HttpPost("UpdateUser")]
        public ContentResult UpdateUser([FromBody]ReqUpdateUser reqUpdateUser)
        {
            string response;
            try
            {
                if (reqUpdateUser != null && ModelState.IsValid)
                {
                    response = userBAL.UpdateUser(reqUpdateUser);
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
            catch (Exception ex)
            {
                return APIResponse.JsonInternelServerErrorResponse(Request, ex);


            }
        }
    }
}