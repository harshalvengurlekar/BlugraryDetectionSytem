using BlugraryDetectionSystemBAL.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BlugraryDetectionSystemBAL.Implementation.Cryptography
{
    public class RCF2898AlgorithmBAL : ICryptographyBAL
    {

        public string DecryptData(string ecnryptedMessage, string salt)
        {
            string decryptedData;
            try
            {
                byte[] cipherBytes = Convert.FromBase64String(ecnryptedMessage);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(salt, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        decryptedData = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
            }
            catch(Exception ex)
            {
                decryptedData = null;
                throw;
            }
            return decryptedData;
        }

        public string EncryptData(string message, out string salt)
        {
            string encryptedString;
            try
            {
                salt = this.GenerateSalt();
                byte[] clearBytes = Encoding.Unicode.GetBytes(message);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(salt, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        encryptedString = Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                salt = encryptedString = null;
                throw;
            }
            return encryptedString;
        }

        //salt is generated using 
        private string GenerateSalt()
        {

            string salt = string.Empty;
            try
            {
                Random robj = new Random();
                int rNumber = robj.Next();
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(Convert.ToString(rNumber));
                salt = System.Convert.ToBase64String(plainTextBytes);
              
            }
            catch (Exception ex)
            {
                salt = "";
                throw;
            }
            return salt;
        }

    }
}
