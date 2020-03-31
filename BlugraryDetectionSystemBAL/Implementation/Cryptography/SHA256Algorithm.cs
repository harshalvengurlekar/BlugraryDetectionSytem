using BlugraryDetectionSystemBAL.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BlugraryDetectionSystemBAL.Implementation.Cryptography
{
    public class SHA256Algorithm : ICryptographyBAL
    {
        public string EncryptData(string message, out string salt)
        {
            string encryptedData = null;
            salt = this.GenerateSalt();
            try
            {
                if (message == null)
                {
                    return null;
                }
                // Get the bytes of the string
                var messageToBeEncrypted = Encoding.UTF8.GetBytes(message);
                var saltBytes = Encoding.UTF8.GetBytes(salt);

                // Hash the salt with SHA256
                saltBytes = SHA256.Create().ComputeHash(saltBytes);
                var bytesEncrypted = this.AESEncrypt(messageToBeEncrypted, saltBytes);
                encryptedData = Convert.ToBase64String(bytesEncrypted);
            }
            catch (Exception ex)
            {
            }
            return encryptedData;
        }

        /// <summary>
        /// Decrypt a string.
        /// </summary>
        /// <param name="encryptedText">String to be decrypted</param>
        /// <param name="password">Password used during encryption</param>
        /// <exception cref="FormatException"></exception>
        public string DecryptData(string ecnryptedMessage, string salt)
        {
            String decryptedMessage = null;
            try
            {
                if (ecnryptedMessage == null && salt == null)
                {
                    return null;
                }

                // Get the bytes of the string
                var bytesToBeDecrypted = Convert.FromBase64String(ecnryptedMessage);
                var passwordBytes = Encoding.UTF8.GetBytes(salt);

                passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

                var bytesDecrypted = this.AESDecrypt(bytesToBeDecrypted, passwordBytes);
                
                decryptedMessage = Encoding.UTF8.GetString(bytesDecrypted);
            }
            catch(Exception ex)
            {

            }
            return decryptedMessage;
        }


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
            }
            return salt;
        }


        //actual logic of decryption of sha
        private byte[] AESEncrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;
            try
            {

                // Set your salt here, change it to meet your flavor:
                // The salt bytes must be at least 8 bytes.
                var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

                using (MemoryStream ms = new MemoryStream())
                {
                    using (RijndaelManaged AES = new RijndaelManaged())
                    {
                        var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                        AES.KeySize = 256;
                        AES.BlockSize = 128;
                        AES.Key = key.GetBytes(AES.KeySize / 8);
                        AES.IV = key.GetBytes(AES.BlockSize / 8);
                        AES.Mode = CipherMode.CBC;

                        using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                            cs.Close();
                        }

                        encryptedBytes = ms.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return encryptedBytes;
        }

        //actual logic of decryption of sha
        private byte[] AESDecrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;
            try
            {
                // Set your salt here, change it to meet your flavor:
                // The salt bytes must be at least 8 bytes.
                var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

                using (MemoryStream ms = new MemoryStream())
                {
                    using (RijndaelManaged AES = new RijndaelManaged())
                    {
                        var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                        AES.KeySize = 256;
                        AES.BlockSize = 128;
                        AES.Key = key.GetBytes(AES.KeySize / 8);
                        AES.IV = key.GetBytes(AES.BlockSize / 8);
                        AES.Mode = CipherMode.CBC;

                        using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                            cs.Close();
                        }

                        decryptedBytes = ms.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return decryptedBytes;
        }
    }
}
