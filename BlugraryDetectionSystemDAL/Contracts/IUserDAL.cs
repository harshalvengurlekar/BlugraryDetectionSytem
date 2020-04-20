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
        DataSet AddUser(ReqAddUser reqAddUser);

        DataSet GetUserPassword(ReqUserAuth reqUserAuth);

        DataSet GetAllUsers();

        DataSet DeleteUser(ReqDeleteUser reqDeleteUser);

        DataSet UpdateUser(ReqUpdateUser reqUpdateUser);

    }
}
