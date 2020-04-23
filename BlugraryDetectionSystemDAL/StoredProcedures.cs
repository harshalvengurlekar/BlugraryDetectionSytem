using System;
using System.Collections.Generic;
using System.Text;

namespace BlugraryDetectionSystemDAL
{
    public static class StoredProcedures
    {
        public const string RegisterUser = "SP_AddUserDetails";

        public const string AuthenticateUser = "SP_AuthenticateUser";

        public const string AddResidents = "SP_AddResidents";

        public const string GetUserResidents = "SP_GetResidents";

        public const string GetAllUsers = "SP_GetAllUsers";

        public const string DeleteUser = "SP_DeleteUser";

        public const string UpdateUser = "SP_UpdateUser";

        public const string GetAllResidents = "SP_GetAllResidents";

        public const string CheckUserNameAvailability = "SP_CheckUserNameAvailability";

        public const string GetLoggedInUserInfo = "SP_GetLoggedInUserInfo";

        public const string UpdateLoggedInUserInfo = "SP_UpdateLoggedInUserInfo";
    }
}
