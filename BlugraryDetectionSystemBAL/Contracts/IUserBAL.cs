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
        string AddUser(ReqAddUser reqAddUser);

        bool AuthenticateUser(ReqUserAuth reqUserAuth, ref string userId, ref string role);

        List<ResAllUsers> GetAllUsers();

        string DeleteUser(ReqDeleteUser reqDeleteUser);

        string UpdateUser(ReqUpdateUser reqUpdateUser);
    }
}
