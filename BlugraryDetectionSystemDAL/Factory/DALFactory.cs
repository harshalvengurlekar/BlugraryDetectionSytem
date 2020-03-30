using BlugraryDetectionSystemDAL.Contracts;
using BlugraryDetectionSystemDAL.Implementation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlugraryDetectionSystemDAL.Factory
{
    public static class DALFactory
    {

        public static IUserDAL GetUserDALObj(string connectionStr)
        {
            return new UserDAL(connectionStr);
        }

    }
}
