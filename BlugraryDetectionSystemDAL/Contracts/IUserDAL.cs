using BlugraryDetectionSystemEntities;
using BlugraryDetectionSystemEntities.RequestEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BlugraryDetectionSystemDAL.Contracts
{
    public interface IUserDAL
    {
        DataSet RegisterUser(ReqRegisterUser reqRegisterUser);

        DataSet GetUserPassword(ReqUserAuth reqUserAuth);

        DataSet GetAllUsers();

        DataSet DeleteUser(ReqDeleteUser reqDeleteUser);

        DataSet UpdateUser(ReqUpdateUser reqUpdateUser);

        DataSet UserNameAvailability(ReqUserNameAvailability reqUserNameAvailability);

        DataSet GetLoggedInUserInfo(ReqGetLoggedUserInfo reqGetLoggedUserInfo);

        DataSet UpdateLoggedInUserInfo(ReqUpdateLoggedInUserInfo reqUpdateLoggedInUserInfo);

    }
}
