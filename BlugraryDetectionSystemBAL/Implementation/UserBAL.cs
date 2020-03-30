using BlugraryDetectionSystemApi.MiscClasses;
using BlugraryDetectionSystemBAL.Contracts;
using BlugraryDetectionSystemDAL.Contracts;
using BlugraryDetectionSystemDAL.Factory;
using BlugraryDetectionSystemEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BlugraryDetectionSystemBAL.Implementation
{
    public class UserBAL : IUserBAL
    {
        private AppSettings appSettings;

        public UserBAL(AppSettings _appSettings)
        {
            this.appSettings = _appSettings;
        }

        public string AddUser(ReqAddUser reqAddUser)
        {
            string respose = "";
            DataSet result;
            try
            {
                result = this.AddUserDB(reqAddUser);
            }
            catch(Exception ex)
            {

            }
            return respose;
        }

        private DataSet AddUserDB(ReqAddUser reqAddUser)
        {
            IUserDAL userDAL;
            DataSet result;
            try
            {
                userDAL = DALFactory.GetUserDALObj(appSettings.appKeys.dbConnectionString);
                result = userDAL.AddUser(reqAddUser);
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }
    }
}
