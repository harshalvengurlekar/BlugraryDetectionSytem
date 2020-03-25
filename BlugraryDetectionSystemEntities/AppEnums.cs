using System;
using System.Collections.Generic;
using System.Text;

namespace BlugraryDetectionSystemEntities
{
    public static class AppEnums
    {

        //All the response codes being used in api
        public enum ResponseCodes
        {
            Success = 200,
            BadRequest = 400,
            UnAuthorized = 401,
            Forbidden = 403,
            NotFound = 404,
            InternalServerError = 500
        }


    }
}
