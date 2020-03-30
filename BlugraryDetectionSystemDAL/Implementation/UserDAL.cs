using BlugraryDetectionSystemDAL.Contracts;
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

        private string connectionStr;
        public UserDAL(string _conntectionStr)
        {
            this.connectionStr = _conntectionStr;
        }


        public DataSet AddUser(ReqAddUser reqAddUser)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionStr))
                {
                    SqlCommand sqlComm = new SqlCommand(StoredProcedures.AddUser, conn);
                    sqlComm.Parameters.AddWithValue("@username", reqAddUser.UserName);
                    sqlComm.Parameters.AddWithValue("@password", reqAddUser.Password);
                    sqlComm.Parameters.AddWithValue("@name", reqAddUser.Name);
                    sqlComm.Parameters.AddWithValue("@age", reqAddUser.Age);
                    sqlComm.Parameters.AddWithValue("@roleid", reqAddUser.RoleId);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;

                    da.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                ds = null;
            }
            return ds;
        }
    }
}
