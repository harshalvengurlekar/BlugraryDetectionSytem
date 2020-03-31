using BlugraryDetectionSystemApi.MiscClasses;
using BlugraryDetectionSystemBAL.Contracts;
using BlugraryDetectionSystemBAL.Factory;
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
        private ICryptographyBAL cryptographyBAL;
        private IUserDAL userDAL;

        public UserBAL(AppSettings _appSettings)
        {
            this.appSettings = _appSettings;
            this.cryptographyBAL = BALFactory.GetRCF2898AlgorithmBALObj();
            this.userDAL = DALFactory.GetUserDALObj(this.appSettings.appKeys.dbConnectionString);
        }

        public string AddUser(ReqAddUser reqAddUser)
        {
            string respose;
            DataSet result;
            try
            {
                result = this.AddUserDB(reqAddUser);
                if(result != null && result.Tables != null && result.Tables.Count == 1 && result.Tables[0].Rows != null && result.Tables[0].Rows.Count == 1)
                {
                    if (result.Tables[0].Rows[0].Field<string>("Action").ToLower() == "inserted" && result.Tables[0].Rows[0].Field<int>("Count") > 0)
                    {
                        respose = "User added sucessfully";
                    }
                    else
                    {
                        respose = "User additon failed";
                    }
                }
                else
                {
                    respose = null;
                }
            }
            catch(Exception ex)
            {
                respose = null;
            }
            return respose;
        }

        private DataSet AddUserDB(ReqAddUser reqAddUser)
        {
            DataSet result;
            string salt;
            try
            {
                reqAddUser.Password = cryptographyBAL.EncryptData(reqAddUser.Password, out salt);
                reqAddUser.SetSalt(salt);
                result = this.userDAL.AddUser(reqAddUser);
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }
    }
}
