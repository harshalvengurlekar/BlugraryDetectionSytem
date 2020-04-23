using BlugraryDetectionSystemEntities;
using BlugraryDetectionSystemEntities.RequestEntities;
using BlugraryDetectionSystemEntities.ResponseEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlugraryDetectionSystemBAL.Contracts
{
    public interface IUserBAL
    {
        //adds user to the database
        string AddUser(ReqRegisterUser reqRegisterUser);

        bool AuthenticateUser(ReqUserAuth reqUserAuth, ref string userId, ref string role);

        List<ResAllUsers> GetAllUsers();

        bool UserNameAvailability(ReqUserNameAvailability reqUserNameAvailability);

        string UpdateLoggedInUserInfo(ReqUpdateLoggedInUserInfo reqUpdateLoggedInUserInfo);

        ResGetLoggedInUserInfo GetLoggedInUserInfo(ReqGetLoggedUserInfo reqGetLoggedUserInfo);

        string DeleteUser(ReqDeleteUser reqDeleteUser);

        string UpdateUser(ReqUpdateUser reqUpdateUser);
    }
}
