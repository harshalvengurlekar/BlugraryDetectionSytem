using BlugraryDetectionSystemApi.MiscClasses;
using BlugraryDetectionSystemBAL.Contracts;
using BlugraryDetectionSystemBAL.Factory;
using BlugraryDetectionSystemDAL.Contracts;
using BlugraryDetectionSystemDAL.Factory;
using BlugraryDetectionSystemEntities;
using BlugraryDetectionSystemEntities.RequestEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BlugraryDetectionSystemBAL.Implementation
{
    public class UserBAL : IUserBAL
    {
        private AppSettings appSettings;
        private ICryptographyBAL sha256cryptographyBAL;
        private ICryptographyBAL aescryptographyBAL;
        private IUserDAL userDAL;

        public UserBAL(AppSettings _appSettings)
        {
            this.appSettings = _appSettings;
            this.sha256cryptographyBAL = BALFactory.GetSHA256BALObj();
            this.aescryptographyBAL = BALFactory.GetAESAlgorithmBALObj(this.appSettings);
            this.userDAL = DALFactory.GetUserDALObj(this.appSettings.appKeys.dbConnectionString);
        }

        public bool AuthenticateUser(ReqUserAuth reqUserAuth,ref string userId)
        {
            bool isAuthenticated = false;
            DataSet result;
            try
            {
                reqUserAuth.UserName = aescryptographyBAL.DecryptData(reqUserAuth.UserName, this.appSettings.appKeys.aesPrivateKey);
                reqUserAuth.Password = aescryptographyBAL.DecryptData(reqUserAuth.Password, this.appSettings.appKeys.aesPrivateKey);
                result = this.GetUsenamePasswordDB(reqUserAuth);
                if (result != null && result.Tables != null && result.Tables.Count == 1 && result.Tables[0].Rows != null && result.Tables[0].Rows.Count == 1)
                {
                    if (reqUserAuth.UserName.Equals(result.Tables[0].Rows[0].Field<string>("UserName")))
                    {
                        string dbPassword = sha256cryptographyBAL.DecryptData(result.Tables[0].Rows[0].Field<string>("Password"), result.Tables[0].Rows[0].Field<string>("Salt"));
                        if (dbPassword.Equals(reqUserAuth.Password))
                        {
                            userId = result.Tables[0].Rows[0].Field<int>("UserID").ToString();
                            isAuthenticated = true;
                        }
                      
                    }
                 
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return isAuthenticated;
        }


        public string AddUser(ReqAddUser reqAddUser)
        {
            string respose;
            DataSet result;
            try
            {
                result = this.AddUserDB(reqAddUser);
                if (result != null && result.Tables != null && result.Tables.Count == 1 && result.Tables[0].Rows != null && result.Tables[0].Rows.Count == 1)
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
            catch (Exception ex)
            {
                respose = null;
                throw;
            }
            return respose;
        }

        private DataSet AddUserDB(ReqAddUser reqAddUser)
        {
            DataSet result;
            string salt;
            try
            {
                reqAddUser.Password = sha256cryptographyBAL.EncryptData(reqAddUser.Password, out salt);
                reqAddUser.SetSalt(salt);
                result = this.userDAL.AddUser(reqAddUser);
            }
            catch (Exception ex)
            {
                result = null;
                throw;
            }
            return result;
        }

        private DataSet GetUsenamePasswordDB(ReqUserAuth reqUserAuth)
        {
            DataSet result;
            try
            {
                result = this.userDAL.GetUserPassword(reqUserAuth);
            }
            catch (Exception ex)
            {
                result = null;
                throw;
            }
            return result;
        }

    }
}
