using BlugraryDetectionSystemDAL.Contracts;
using BlugraryDetectionSystemDAL.Factory;
using BlugraryDetectionSystemEntities.RequestEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BlugraryDetectionSystemDAL.Implementation
{
    public class ResidentDAL : IResidentsDAL
    {
        string connectionStr;
        DBHelper dBHelper;

        public ResidentDAL(string _connectionStr)
        {
            this.connectionStr = _connectionStr;
            this.dBHelper = DALFactory.GetDBHelper(this.connectionStr);
        }


       public DataSet GetUserResidents(ReqGetUserResidents reqGetResidents)
        {
            DataSet resultDs = null;
            IDictionary<string, object> inputParams = new Dictionary<string, object>();
            try
            {
                inputParams.Add("@userid", reqGetResidents.UserID);

                resultDs = dBHelper.ExecuteStoredProcedure(StoredProcedures.GetAllResidents, inputParams);
            }
            catch (Exception ex)
            {
                throw;
            }
            return resultDs;
        }

        public DataSet AddResidents(ReqAddResidents reqAddResidents)
        {
            DataSet resultDs = null;
            IDictionary<string, object> inputParams = new Dictionary<string, object>();
            try
            {
                inputParams.Add("@residentname", reqAddResidents.ResidentName);
                inputParams.Add("@residentimage", reqAddResidents.GetFilePath());
                inputParams.Add("@userid", reqAddResidents.UserId);
              
                resultDs = dBHelper.ExecuteStoredProcedure(StoredProcedures.AddResidents, inputParams);
            }
            catch (Exception ex)
            {
                throw;
            }
            return resultDs;
        }
    }
}
