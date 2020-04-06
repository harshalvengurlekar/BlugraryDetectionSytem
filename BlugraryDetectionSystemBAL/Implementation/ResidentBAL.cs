﻿using BlugraryDetectionSystemApi.MiscClasses;
using BlugraryDetectionSystemBAL.Contracts;
using BlugraryDetectionSystemBAL.Factory;
using BlugraryDetectionSystemDAL.Contracts;
using BlugraryDetectionSystemDAL.Factory;
using BlugraryDetectionSystemEntities.RequestEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace BlugraryDetectionSystemBAL.Implementation
{
    public class ResidentBAL : IResidentBAL 
    {
        private AppSettings appSettings;
        private IResidentsDAL residentsDAL;
        private ICryptographyBAL aesBAL;

        public ResidentBAL(AppSettings _appsettings)
        {
            this.appSettings = _appsettings;
            this.residentsDAL = DALFactory.GetResidentsDALObj(appSettings.appKeys.dbConnectionString);
            this.aesBAL = BALFactory.GetAESAlgorithmBALObj(this.appSettings);
        }

       public string AddResident(ReqAddResidents reqAddResident)
        {
            string respose;
            DataSet result;
            try
            {
                string dateTime = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                reqAddResident.ResidentName = aesBAL.DecryptData(reqAddResident.ResidentName,appSettings.appKeys.aesPrivateKey);
                reqAddResident.UserId = aesBAL.DecryptData(reqAddResident.UserId, appSettings.appKeys.aesPrivateKey);
                Byte[] bytes = Convert.FromBase64String(reqAddResident.FileBase64);
                if(!File.Exists("UserImages/" + reqAddResident.UserId))
                {
                    System.IO.Directory.CreateDirectory("UserImages/" + reqAddResident.UserId);
                }
                File.WriteAllBytes("UserImages/" +reqAddResident.UserId + @"/" +reqAddResident.ResidentName + dateTime + ".bmp", bytes);
                reqAddResident.SetFilePath(reqAddResident.UserId + @"/" + reqAddResident.ResidentName + dateTime + ".bmp");
                result = this.AddResidentDB(reqAddResident);
                if (result != null && result.Tables != null && result.Tables.Count == 1 && result.Tables[0].Rows != null && result.Tables[0].Rows.Count == 1)
                {
                    if (result.Tables[0].Rows[0].Field<string>("Action").ToLower() == "inserted" && result.Tables[0].Rows[0].Field<int>("Count") > 0)
                    {
                        respose = "Resident added sucessfully";
                    }
                    else
                    {
                        respose = "Resident additon failed";
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


        private DataSet AddResidentDB(ReqAddResidents reqAddResident)
        {
            DataSet result;
            try
            {
                result = this.residentsDAL.AddResidents(reqAddResident);
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