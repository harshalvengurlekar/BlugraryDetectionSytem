﻿using BlugraryDetectionSystemApi.MiscClasses;
using BlugraryDetectionSystemBAL.Contracts;
using BlugraryDetectionSystemBAL.Factory;
using BlugraryDetectionSystemDAL.Contracts;
using BlugraryDetectionSystemDAL.Factory;
using BlugraryDetectionSystemEntities.RequestEntities;
using BlugraryDetectionSystemEntities.ResponseEntities;
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
        private ICryptographyBAL aesCryptographyBAL;

        public ResidentBAL(AppSettings _appsettings)
        {
            this.appSettings = _appsettings;
            this.residentsDAL = DALFactory.GetResidentsDALObj(appSettings.appKeys.dbConnectionString);
            this.aesCryptographyBAL = BALFactory.GetAESAlgorithmBALObj(this.appSettings);
        }

        public string AddResident(ReqAddResidents reqAddResident)
        {
            string respose;
            DataSet result;
            try
            {
                string dateTime = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                reqAddResident.ResidentName = aesCryptographyBAL.DecryptData(reqAddResident.ResidentName, appSettings.appKeys.aesPrivateKey);
                reqAddResident.UserId = aesCryptographyBAL.DecryptData(reqAddResident.UserId, appSettings.appKeys.aesPrivateKey);
                Byte[] bytes = Convert.FromBase64String(reqAddResident.FileBase64);
                if (!File.Exists("UserImages/" + reqAddResident.UserId))
                {
                    System.IO.Directory.CreateDirectory("UserImages/" + reqAddResident.UserId);
                }
                File.WriteAllBytes("UserImages/" + reqAddResident.UserId + @"/" + reqAddResident.ResidentName + dateTime + ".bmp", bytes);
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
        public List<ResGetUserResidents> GetUserResidents(ReqGetUserResidents reqGetUserResidents)
        {
            DataSet result;
            List<ResGetUserResidents> resGetResidents = null;
            try
            {
                result = this.GetUserResidentsDB(reqGetUserResidents);
                if (result != null && result.Tables != null && result.Tables.Count == 1 && result.Tables[0].Rows != null && result.Tables[0].Rows.Count > 0)
                {
                    resGetResidents = new List<ResGetUserResidents>();
                    foreach (var row in result.Tables[0].AsEnumerable())
                    {
                        ResGetUserResidents resident = new ResGetUserResidents();
                        resident.ResidentName = row.Field<string>("ResidentName");
                        string fileName = row.Field<string>("ResidentImage");
                        Byte[] bytes = File.ReadAllBytes("UserImages/" + fileName);
                        resident.ResidentPhotos = Convert.ToBase64String(bytes);
                        resGetResidents.Add(resident);
                    }
                }
            }
            catch (Exception ex)
            {
                resGetResidents = null;
                throw;
            }
            return resGetResidents;

        }

        public List<ResGetAllResidents> GetAllResidents()
        {
            DataSet result;
            List<ResGetAllResidents> resAllResidents = null;
            try
            {
                result = this.GetAllResidentsDB();
                if (result != null && result.Tables != null && result.Tables.Count == 1 && result.Tables[0].Rows != null && result.Tables[0].Rows.Count > 0)
                {
                    string salt;
                    resAllResidents = new List<ResGetAllResidents>();
                    foreach (var row in result.Tables[0].AsEnumerable())
                    {
                        ResGetAllResidents resident = new ResGetAllResidents();
                        resident.ResidentName = aesCryptographyBAL.EncryptData(row.Field<string>("ResidentName"),out salt);
                        resident.FirstName = aesCryptographyBAL.EncryptData(row.Field<string>("FirstName"), out salt);
                        resident.UserID = aesCryptographyBAL.EncryptData(row.Field<string>("UserID"), out salt);
                        resident.UserName = aesCryptographyBAL.EncryptData(row.Field<string>("UserName"), out salt);
                        resAllResidents.Add(resident);
                    }
                }
            }
            catch (Exception ex)
            {
                resAllResidents = null;
                throw;
            }
            return resAllResidents;
        }

        private DataSet GetUserResidentsDB(ReqGetUserResidents reqGetUserResidents)
        {
            DataSet result;
            try
            {
                result = this.residentsDAL.GetUserResidents(reqGetUserResidents);
            }
            catch (Exception ex)
            {
                result = null;
                throw;
            }
            return result;
        }

        private DataSet GetAllResidentsDB()
        {
            DataSet result;
            try
            {
                result = this.residentsDAL.GetAllResidents();
            }
            catch (Exception ex)
            {
                result = null;
                throw;
            }
            return result;
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
