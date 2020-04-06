using BlugraryDetectionSystemEntities;
using BlugraryDetectionSystemEntities.RequestEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlugraryDetectionSystemBAL.Contracts
{
    public interface IUserBAL
    {
        //adds user to the database
        string AddUser(ReqAddUser reqAddUser);

        bool AuthenticateUser(ReqUserAuth reqUserAuth,ref string UserId);
    }
}
