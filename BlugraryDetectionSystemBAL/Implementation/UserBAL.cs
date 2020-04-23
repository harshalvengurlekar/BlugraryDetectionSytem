using BlugraryDetectionSystemApi.MiscClasses;
using BlugraryDetectionSystemBAL.Contracts;
using BlugraryDetectionSystemBAL.Factory;
using BlugraryDetectionSystemDAL.Contracts;
using BlugraryDetectionSystemDAL.Factory;
using BlugraryDetectionSystemEntities;
using BlugraryDetectionSystemEntities.RequestEntities;
using BlugraryDetectionSystemEntities.ResponseEntities;
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

        public bool AuthenticateUser(ReqUserAuth reqUserAuth, ref string userId, ref string role)
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
                            role = result.Tables[0].Rows[0].Field<string>("RoleName").ToString();
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

        public List<ResAllUsers> GetAllUsers()
        {
            List<ResAllUsers> users;
            DataSet result;
            try
            {
                result = this.GetAllUsersDB();
                if (result != null && result.Tables != null && result.Tables.Count == 1 && result.Tables[0].Rows != null && result.Tables[0].Rows.Count > 0)
                {
                    users = new List<ResAllUsers>();
                    foreach (var rows in result.Tables[0].AsEnumerable())
                    {
                        ResAllUsers user = new ResAllUsers();
                        string aesPrivateKey = null;
                        user.UserName = aescryptographyBAL.EncryptData(rows.Field<string>("UserName"), out aesPrivateKey);
                        user.Name = aescryptographyBAL.EncryptData(rows.Field<string>("Name"), out aesPrivateKey);
                        user.RoleName = aescryptographyBAL.EncryptData(rows.Field<string>("RoleName"), out aesPrivateKey);
                        user.Age = aescryptographyBAL.EncryptData(Convert.ToString(rows.Field<int>("Age")), out aesPrivateKey);
                        user.UserID = aescryptographyBAL.EncryptData(Convert.ToString(rows.Field<int>("UserID")), out aesPrivateKey);
                        user.Email = aescryptographyBAL.EncryptData(rows.Field<string>("Email"), out aesPrivateKey);
                        users.Add(user);
                    }
                }
                else
                {
                    users = null;

                }
            }
            catch (Exception ex)
            {
                users = null;
                throw;
            }
            return users;
        }

        public bool UserNameAvailability(ReqUserNameAvailability reqUserNameAvailability)
        {
            bool isavailable = false;
            DataSet result;
            try
            {
                result = this.CheckUserNameAvailability(reqUserNameAvailability);
                if (result != null && result.Tables != null && result.Tables.Count == 1 && result.Tables[0].Rows != null && result.Tables[0].Rows.Count == 1)
                {
                    if (result.Tables[0].Rows[0].Field<string>("Availability").ToLower() == "true")
                    {
                        isavailable = true;
                    }
                }
                else
                {
                    isavailable = false;
                }
            }
            catch (Exception ex)
            {
                isavailable = false;
                throw;
            }
            return isavailable;
        }

        public string AddUser(ReqRegisterUser reqRegisterUser)
        {
            string respose;
            DataSet result;
            try
            {
                result = this.AddUserDB(reqRegisterUser);
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

        public string UpdateLoggedInUserInfo(ReqUpdateLoggedInUserInfo reqUpdateLoggedInUserInfo)
        {
            string respose;
            DataSet result;
            try
            {
                result = this.UpdateLoggedInUserInfoDB(reqUpdateLoggedInUserInfo);
                if (result != null && result.Tables != null && result.Tables.Count == 1 && result.Tables[0].Rows != null && result.Tables[0].Rows.Count == 1)
                {
                    if (result.Tables[0].Rows[0].Field<string>("Action").ToLower() == "updated")
                    {
                        if (result.Tables[0].Rows[0].Field<int>("Count") > 0)
                            respose = "User updated sucessfully";
                        else
                            respose = "No such user exists";
                    }
                    else
                    {
                        respose = "User updation failed";
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

        public ResGetLoggedInUserInfo GetLoggedInUserInfo(ReqGetLoggedUserInfo reqGetLoggedUserInfo)
        {
            ResGetLoggedInUserInfo user;
            DataSet result;
            try
            {
                result = this.GetLoggedInUserInfoDB(reqGetLoggedUserInfo);
                if (result != null && result.Tables != null && result.Tables.Count == 1 && result.Tables[0].Rows != null && result.Tables[0].Rows.Count == 1)
                {
                    user = new ResGetLoggedInUserInfo();
                    string aesPrivateKey = null;
                    user.UserName = aescryptographyBAL.EncryptData(result.Tables[0].Rows[0].Field<string>("UserName"), out aesPrivateKey);
                    user.Name = aescryptographyBAL.EncryptData(result.Tables[0].Rows[0].Field<string>("Name"), out aesPrivateKey);
                    user.Age = aescryptographyBAL.EncryptData(Convert.ToString(result.Tables[0].Rows[0].Field<int>("Age")), out aesPrivateKey);
                    user.Email = aescryptographyBAL.EncryptData(result.Tables[0].Rows[0].Field<string>("Email"), out aesPrivateKey);
                    user.Password = aescryptographyBAL.EncryptData(sha256cryptographyBAL.DecryptData(result.Tables[0].Rows[0].Field<string>("Password"), result.Tables[0].Rows[0].Field<string>("Salt")),out aesPrivateKey);
                    user.UserID = aescryptographyBAL.EncryptData(Convert.ToString(result.Tables[0].Rows[0].Field<int>("UserID")), out aesPrivateKey);

                }
                else
                {
                    user = null;

                }
            }
            catch (Exception ex)
            {
                user = null;
                throw;
            }
            return user;
        }



        public string DeleteUser(ReqDeleteUser reqDeleteUser)
        {
            string response;
            DataSet result;
            try
            {
                result = this.DeleteUserDB(reqDeleteUser);
                if (result != null && result.Tables != null && result.Tables.Count == 1 && result.Tables[0].Rows != null && result.Tables[0].Rows.Count == 1)
                {
                    if (result.Tables[0].Rows[0].Field<string>("Action").ToLower() == "deleted")
                    {
                        if (result.Tables[0].Rows[0].Field<int>("Count") > 0)
                            response = "User deleted sucessfully";
                        else if (result.Tables[0].Rows[0].Field<int>("Count") == 0)
                            response = "User doesn't exist";
                        else
                            response = null;
                    }
                    else
                    {
                        response = "User deletion failed";
                    }
                }
                else
                {
                    response = null;
                }
            }
            catch (Exception ex)
            {
                response = null;
                throw;
            }
            return response;
        }

        public string UpdateUser(ReqUpdateUser reqUpdateUser)
        {
            string response;
            DataSet result;
            try
            {

                result = this.UpdateUserDB(reqUpdateUser);
                if (result != null && result.Tables != null && result.Tables.Count == 1 && result.Tables[0].Rows != null && result.Tables[0].Rows.Count == 1)
                {
                    if (result.Tables[0].Rows[0].Field<string>("Action").ToLower() == "updated")
                    {
                        if (result.Tables[0].Rows[0].Field<int>("Count") > 0)
                            response = "User updated sucessfully";
                        else if (result.Tables[0].Rows[0].Field<int>("Count") == 0)
                            response = "User doesn't exist";
                        else
                            response = null;
                    }
                    else
                    {
                        response = "User updation failed";
                    }
                }
                else
                {
                    response = null;
                }
            }
            catch (Exception ex)
            {
                response = null;
                throw;
            }
            return response;
        }


        private DataSet UpdateLoggedInUserInfoDB(ReqUpdateLoggedInUserInfo reqUpdateLoggedInUserInfo)
        {
            DataSet result;
            string salt;
            try
            {
                reqUpdateLoggedInUserInfo.UserName = aescryptographyBAL.DecryptData(reqUpdateLoggedInUserInfo.UserName, appSettings.appKeys.aesPrivateKey);
                reqUpdateLoggedInUserInfo.Name = aescryptographyBAL.DecryptData(reqUpdateLoggedInUserInfo.Name, appSettings.appKeys.aesPrivateKey);
                reqUpdateLoggedInUserInfo.Age = aescryptographyBAL.DecryptData(reqUpdateLoggedInUserInfo.Age, appSettings.appKeys.aesPrivateKey);
                reqUpdateLoggedInUserInfo.UserID = aescryptographyBAL.DecryptData(reqUpdateLoggedInUserInfo.UserID, appSettings.appKeys.aesPrivateKey);
                reqUpdateLoggedInUserInfo.Email = aescryptographyBAL.DecryptData(reqUpdateLoggedInUserInfo.Email, appSettings.appKeys.aesPrivateKey);
                reqUpdateLoggedInUserInfo.Password = aescryptographyBAL.DecryptData(reqUpdateLoggedInUserInfo.Password, appSettings.appKeys.aesPrivateKey);
                reqUpdateLoggedInUserInfo.Password = sha256cryptographyBAL.EncryptData(reqUpdateLoggedInUserInfo.Password, out salt);
                reqUpdateLoggedInUserInfo.SetSlat(salt);
                result = this.userDAL.UpdateLoggedInUserInfo(reqUpdateLoggedInUserInfo);
            }
            catch (Exception ex)
            {
                result = null;
                throw;
            }
            return result;
        }


        private DataSet GetLoggedInUserInfoDB(ReqGetLoggedUserInfo reqGetLoggedUserInfo)
        {
            DataSet result;
            try
            {
                reqGetLoggedUserInfo.UserName = aescryptographyBAL.DecryptData(reqGetLoggedUserInfo.UserName, appSettings.appKeys.aesPrivateKey);
                result = this.userDAL.GetLoggedInUserInfo(reqGetLoggedUserInfo);
            }
            catch (Exception ex)
            {
                result = null;
                throw;
            }
            return result;
        }


        private DataSet AddUserDB(ReqRegisterUser reqRegisterUser)
        {
            DataSet result;
            string salt;
            try
            {
                reqRegisterUser.Age = aescryptographyBAL.DecryptData(reqRegisterUser.Age, appSettings.appKeys.aesPrivateKey);
                reqRegisterUser.Email = aescryptographyBAL.DecryptData(reqRegisterUser.Email, appSettings.appKeys.aesPrivateKey);
                reqRegisterUser.Name = aescryptographyBAL.DecryptData(reqRegisterUser.Name, appSettings.appKeys.aesPrivateKey);
                reqRegisterUser.Password = aescryptographyBAL.DecryptData(reqRegisterUser.Password, appSettings.appKeys.aesPrivateKey);
                reqRegisterUser.UserName = aescryptographyBAL.DecryptData(reqRegisterUser.UserName, appSettings.appKeys.aesPrivateKey);



                reqRegisterUser.Password = sha256cryptographyBAL.EncryptData(reqRegisterUser.Password, out salt);
                reqRegisterUser.SetSalt(salt);
                result = this.userDAL.RegisterUser(reqRegisterUser);
            }
            catch (Exception ex)
            {
                result = null;
                throw;
            }
            return result;
        }

        private DataSet UpdateUserDB(ReqUpdateUser reqUpdateUser)
        {
            DataSet result;
            try
            {
                reqUpdateUser.Age = aescryptographyBAL.DecryptData(reqUpdateUser.Age, appSettings.appKeys.aesPrivateKey);
                reqUpdateUser.Name = aescryptographyBAL.DecryptData(reqUpdateUser.Name, appSettings.appKeys.aesPrivateKey);
                reqUpdateUser.UserType = aescryptographyBAL.DecryptData(reqUpdateUser.UserType, appSettings.appKeys.aesPrivateKey);
                reqUpdateUser.UserName = aescryptographyBAL.DecryptData(reqUpdateUser.UserName, appSettings.appKeys.aesPrivateKey);
                reqUpdateUser.Email = aescryptographyBAL.DecryptData(reqUpdateUser.Email, appSettings.appKeys.aesPrivateKey);
                result = this.userDAL.UpdateUser(reqUpdateUser);
            }
            catch (Exception ex)
            {
                result = null;
                throw;
            }
            return result;
        }

        private DataSet DeleteUserDB(ReqDeleteUser reqDeleteUser)
        {
            DataSet result;
            try
            {
                reqDeleteUser.UserName = aescryptographyBAL.DecryptData(reqDeleteUser.UserName, appSettings.appKeys.aesPrivateKey);
                result = this.userDAL.DeleteUser(reqDeleteUser);
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

        private DataSet GetAllUsersDB()
        {

            DataSet result;
            try
            {
                result = this.userDAL.GetAllUsers();
            }
            catch (Exception ex)
            {
                result = null;
                throw;
            }
            return result;
        }

        private DataSet CheckUserNameAvailability(ReqUserNameAvailability reqUserNameAvailability)
        {

            DataSet result;
            try
            {
                reqUserNameAvailability.UserName = aescryptographyBAL.DecryptData(reqUserNameAvailability.UserName, appSettings.appKeys.aesPrivateKey);
                result = this.userDAL.UserNameAvailability(reqUserNameAvailability);
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
