using BlugraryDetectionSystemDAL.Contracts;
using BlugraryDetectionSystemDAL.Factory;
using BlugraryDetectionSystemEntities;
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
            }
            return resultDs;
        }
    }
}
