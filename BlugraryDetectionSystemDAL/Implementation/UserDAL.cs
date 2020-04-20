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

        public DataSet AddUser(ReqAddUser reqAddUser)
        {
            DataSet resultDs = null;
            IDictionary<string, object> inputParams = new Dictionary<string, object>();
            try
            {
                inputParams.Add("@username", reqAddUser.UserName);
                inputParams.Add("@password", reqAddUser.Password);
                inputParams.Add("@name", reqAddUser.Name);
                inputParams.Add("@age", reqAddUser.Age);
                inputParams.Add("@roleid", reqAddUser.RoleId);
                inputParams.Add("@salt",reqAddUser.GetSalt());
                resultDs = dBHelper.ExecuteStoredProcedure(StoredProcedures.AddUser, inputParams);
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
                resultDs = dBHelper.ExecuteStoredProcedure(StoredProcedures.GetAllUsers,inputParams);
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
                inputParams.Add("@name", reqUpdateUser.Name);
                inputParams.Add("@type", reqUpdateUser.UserType);
                resultDs = dBHelper.ExecuteStoredProcedure(StoredProcedures.AddUser, inputParams);
            }
            catch (Exception ex)
            {
                throw;
            }
            return resultDs;
        }
    }
}
