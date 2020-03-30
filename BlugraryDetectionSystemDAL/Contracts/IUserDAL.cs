using BlugraryDetectionSystemEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BlugraryDetectionSystemDAL.Contracts
{
    public interface IUserDAL
    {
        DataSet AddUser(ReqAddUser reqAddUser);

    }
}
