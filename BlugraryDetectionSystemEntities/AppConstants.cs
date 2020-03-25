using System;
using System.Collections.Generic;
using System.Text;

namespace BlugraryDetectionSystemEntities
{
    public static class AppConstants
    {
        public static class ContentTypes
        {
            public const string Application_JSON = "application/json"; 
        }

        public static class APIResponseMessages
        {
            public const string NotFoundResponseMsg = "Resource not found";

            public const string UnAuthorizedResponseMsg = "Authorization failed";

            public const string ForbiddenResponseMsg = "You don't have permission to access this resource";
        }

    }
}
