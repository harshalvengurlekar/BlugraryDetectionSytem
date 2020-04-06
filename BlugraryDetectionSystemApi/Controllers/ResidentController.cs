﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlugraryDetectionSystemApi.MiscClasses;
using BlugraryDetectionSystemBAL.Contracts;
using BlugraryDetectionSystemBAL.Factory;
using BlugraryDetectionSystemEntities.RequestEntities;
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

        [Authorize]
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

    }
}