using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BlugraryDetectionSystemDAL
{
    public class DBHelper
    {

        private string connectionStr { get; set; }

        public DBHelper(string _connectionStr)
        {
            this.connectionStr = _connectionStr;
        }


        public DataSet ExecuteStoredProcedure(string storedProcedureName,IDictionary<string,object> parameters)
        {
            DataSet result = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionStr))
                {
                    SqlCommand sqlComm = new SqlCommand(storedProcedureName, conn);
                    foreach(var parameter in parameters )
                    {
                        sqlComm.Parameters.AddWithValue(parameter.Key,parameter.Value);
                    }
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    da.Fill(result);
                }
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }


    }
}
