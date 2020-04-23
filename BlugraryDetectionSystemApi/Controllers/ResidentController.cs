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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BlugraryDetectionSystemApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ResidentController : ControllerBase
    {

        private IResidentBAL residentBAL;
        private AppSettings appSettings;
        public ResidentController(IOptionsSnapshot<AppSettings> appsettingsOptions)
        {
            this.appSettings = appsettingsOptions.Value;
            this.residentBAL = BALFactory.GetResidentBALObj(appSettings);
        }

        [Authorize(Roles = Role.User)]
        [HttpPost("AddResident"),DisableRequestSizeLimit]
        public ContentResult AddResident([FromBody]ReqAddResidents reqAddResidents)
        {
            string response;
            try
            {
                if (reqAddResidents != null && ModelState.IsValid)
                {
                    response = residentBAL.AddResident(reqAddResidents);
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


        [Authorize(Roles = Role.User)]
        [HttpPost("GetResident"), DisableRequestSizeLimit]
        public ContentResult GetResident([FromBody]ReqGetUserResidents reqGetResidents)
        {
            List<ResGetUserResidents> residents;
            try
            {
                if (reqGetResidents != null && ModelState.IsValid)
                {
                    residents = residentBAL.GetUserResidents(reqGetResidents);
                    if (residents != null)
                        return APIResponse.JsonSuccessResponse(Request, residents);
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
        [HttpPost("GetAllResident"), DisableRequestSizeLimit]
        public ContentResult GetAllResident()
        {
            List<ResGetAllResidents> residents;
            try
            {

                    residents = residentBAL.GetAllResidents();
                    if (residents != null)
                        return APIResponse.JsonSuccessResponse(Request, residents);
                    else
                        return APIResponse.JsonInternelServerErrorResponse(Request, new Exception("Something went wrong"));

            }
            catch (Exception ex)
            {
                return APIResponse.JsonInternelServerErrorResponse(Request, ex);


            }
        }
    }
}