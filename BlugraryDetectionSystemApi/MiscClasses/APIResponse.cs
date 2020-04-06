using BlugraryDetectionSystemEntities;
using BlugraryDetectionSystemEntities.ResponseEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlugraryDetectionSystemApi.MiscClasses
{
    public class APIResponse
    {
        //making the constructor private to restric creation of object
        private APIResponse()
        {

        }

        //code to return success response
        public static ContentResult JsonSuccessResponse(HttpRequest request, string responseString)
        {
            ResStandardResponse response = new ResStandardResponse();
            response.Message = responseString;
            return new ContentResult()
            {
                StatusCode = Convert.ToInt32(AppEnums.ResponseCodes.Success),
                Content = JsonConvert.SerializeObject(response),
                ContentType = AppConstants.ContentTypes.Application_JSON
            };
        }

        //overload to return success response
        public static ContentResult JsonSuccessResponse(HttpRequest request, object responseObject)
        {
            return new ContentResult()
            {
                StatusCode = Convert.ToInt32(AppEnums.ResponseCodes.Success),
                Content = JsonConvert.SerializeObject(responseObject),
                ContentType = AppConstants.ContentTypes.Application_JSON
            };
        }


        //code to return resource not found
        public static ContentResult JsonNotFoundResponse(HttpRequest request)
        {
            ResStandardResponse response = new ResStandardResponse();
            response.Message = AppConstants.APIResponseMessages.NotFoundResponseMsg;
            return new ContentResult()
            {
                StatusCode = Convert.ToInt32(AppEnums.ResponseCodes.NotFound),
                ContentType = AppConstants.ContentTypes.Application_JSON,
                Content = JsonConvert.SerializeObject(response)
            };
        }

        //code to return bad request response
        public static ContentResult JsonBadRequestResponse(HttpRequest request, string responseMessage)
        {
            ResStandardResponse response = new ResStandardResponse();
            response.Message = responseMessage;
            return new ContentResult()
            {
                StatusCode = Convert.ToInt32(AppEnums.ResponseCodes.BadRequest),
                ContentType = AppConstants.ContentTypes.Application_JSON,
                Content = JsonConvert.SerializeObject(response)
            };
        }

        //code to return internal server error response
        public static ContentResult JsonInternelServerErrorResponse(HttpRequest request, Exception exception)
        {
            ResStandardResponse response = new ResStandardResponse();
            response.Message = exception.Message;
            return new ContentResult()
            {
                StatusCode = Convert.ToInt32(AppEnums.ResponseCodes.InternalServerError),
                ContentType = AppConstants.ContentTypes.Application_JSON,
                Content = JsonConvert.SerializeObject(response)
            };
        }


        //code to return unauthorized response
        public static ContentResult JsonUnauthorizedResponse(HttpRequest request)
        {
            ResStandardResponse response = new ResStandardResponse();
            response.Message = AppConstants.APIResponseMessages.UnAuthorizedResponseMsg;
            return new ContentResult()
            {
                StatusCode = Convert.ToInt32(AppEnums.ResponseCodes.UnAuthorized),
                ContentType = AppConstants.ContentTypes.Application_JSON,
                Content = JsonConvert.SerializeObject(response)
            };
        }

        //code to return forbidden response
        public static ContentResult JsonForbiddenResponse(HttpRequest request)
        {
            ResStandardResponse response = new ResStandardResponse();
            response.Message = AppConstants.APIResponseMessages.ForbiddenResponseMsg;
            return new ContentResult()
            {
                StatusCode = Convert.ToInt32(AppEnums.ResponseCodes.Forbidden),
                ContentType = AppConstants.ContentTypes.Application_JSON,
                Content = JsonConvert.SerializeObject(response)
            };
        }
    }

}
