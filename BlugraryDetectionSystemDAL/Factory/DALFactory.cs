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


        public static DBHelper GetDBHelper(string connectionStr)
        {
            return new DBHelper(connectionStr);
        }

        public static IResidentsDAL GetResidentsDALObj(string connectionStr)
        {
            return new ResidentDAL(connectionStr);

        }
    }
}
