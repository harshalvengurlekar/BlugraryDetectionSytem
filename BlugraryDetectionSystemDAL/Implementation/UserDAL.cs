using BlugraryDetectionSystemDAL.Contracts;
using BlugraryDetectionSystemDAL.Factory;
using BlugraryDetectionSystemEntities;
using BlugraryDetectionSystemEntities.RequestEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BlugraryDetectionSystemDAL.Implementation
{
    public class UserDAL : IUserDAL
    {

        private DBHelper dBHelper;
        public UserDAL(string _conntectionStr)
        {
            this.dBHelper = DALFactory.GetDBHelper(_conntectionStr);
        }

        public DataSet UserNameAvailability(ReqUserNameAvailability reqUserNameAvailability)
        {
            DataSet resultDs = null;
            IDictionary<string, object> inputParams = new Dictionary<string, object>();
            try
            {
                inputParams.Add("@username", reqUserNameAvailability.UserName);
                resultDs = dBHelper.ExecuteStoredProcedure(StoredProcedures.CheckUserNameAvailability, inputParams);
            }
            catch (Exception ex)
            {
                throw;
            }
            return resultDs;
        }

        public DataSet GetUserPassword(ReqUserAuth reqUserAuth)
        {
            DataSet resultDs = null;
            IDictionary<string, object> inputParams = new Dictionary<string, object>();
            try
            {
                inputParams.Add("@username", reqUserAuth.UserName);
                resultDs = dBHelper.ExecuteStoredProcedure(StoredProcedures.AuthenticateUser, inputParams);
            }
            catch (Exception ex)
            {
                throw;
            }
            return resultDs;
        }


        public DataSet DeleteUser(ReqDeleteUser reqDeleteUser)
        {
            DataSet resultDs = null;
            IDictionary<string, object> inputParams = new Dictionary<string, object>();
            try
            {
                inputParams.Add("@username", reqDeleteUser.UserName);
                resultDs = dBHelper.ExecuteStoredProcedure(StoredProcedures.DeleteUser, inputParams);
            }
            catch (Exception ex)
            {
                throw;
            }
            return resultDs;
        }

        public DataSet RegisterUser(ReqRegisterUser reqRegisterUser)
        {
            DataSet resultDs = null;
            IDictionary<string, object> inputParams = new Dictionary<string, object>();
            try
            {
                inputParams.Add("@username", reqRegisterUser.UserName);
                inputParams.Add("@password", reqRegisterUser.Password);
                inputParams.Add("@name", reqRegisterUser.Name);
                inputParams.Add("@age", reqRegisterUser.Age);
                inputParams.Add("@roleid", reqRegisterUser.RoleId);
                inputParams.Add("@salt", reqRegisterUser.GetSalt());
                inputParams.Add("@email", reqRegisterUser.Email);
                resultDs = dBHelper.ExecuteStoredProcedure(StoredProcedures.RegisterUser, inputParams);
            }
            catch (Exception ex)
            {
                throw;
            }
            return resultDs;
        }


        public DataSet GetAllUsers()
        {
            DataSet resultDs = null;
            IDictionary<string, object> inputParams = new Dictionary<string, object>();
            try
            {
                resultDs = dBHelper.ExecuteStoredProcedure(StoredProcedures.GetAllUsers, inputParams);
            }
            catch (Exception ex)
            {
                throw;
            }
            return resultDs;
        }

        public DataSet UpdateLoggedInUserInfo(ReqUpdateLoggedInUserInfo reqUpdateLoggedInUserInfo)
        {
            DataSet resultDs = null;
            IDictionary<string, object> inputParams = new Dictionary<string, object>();
            try
            {
                inputParams.Add("@username", reqUpdateLoggedInUserInfo.UserName);
                inputParams.Add("@name", reqUpdateLoggedInUserInfo.Name);
                inputParams.Add("@age", reqUpdateLoggedInUserInfo.Age);
                inputParams.Add("@email", reqUpdateLoggedInUserInfo.Email);
                inputParams.Add("@userid", reqUpdateLoggedInUserInfo.UserID);
                inputParams.Add("@password", reqUpdateLoggedInUserInfo.Password);
                inputParams.Add("@salt", reqUpdateLoggedInUserInfo.GetSalt());

                resultDs = dBHelper.ExecuteStoredProcedure(StoredProcedures.UpdateLoggedInUserInfo, inputParams);
            }
            catch (Exception ex)
            {
                throw;
            }
            return resultDs;
        }

       public DataSet GetLoggedInUserInfo(ReqGetLoggedUserInfo reqGetLoggedUserInfo)
        {
            DataSet resultDs = null;
            IDictionary<string, object> inputParams = new Dictionary<string, object>();
            try
            {
                inputParams.Add("@username", reqGetLoggedUserInfo.UserName);
                resultDs = dBHelper.ExecuteStoredProcedure(StoredProcedures.GetLoggedInUserInfo, inputParams);
            }
            catch (Exception ex)
            {
                throw;
            }
            return resultDs;
        }

        public DataSet UpdateUser(ReqUpdateUser reqUpdateUser)
        {
            DataSet resultDs = null;
            IDictionary<string, object> inputParams = new Dictionary<string, object>();
            try
            {
                inputParams.Add("@username", reqUpdateUser.UserName);
                inputParams.Add("@name", reqUpdateUser.Name);
                inputParams.Add("@age", reqUpdateUser.Age);
                inputParams.Add("@email", reqUpdateUser.Email);
                inputParams.Add("@type", reqUpdateUser.UserType);
                resultDs = dBHelper.ExecuteStoredProcedure(StoredProcedures.UpdateUser, inputParams);
            }
            catch (Exception ex)
            {
                throw;
            }
            return resultDs;
        }
    }
}
