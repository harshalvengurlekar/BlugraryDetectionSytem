using BlugraryDetectionSystemApi.MiscClasses;
using BlugraryDetectionSystemBAL.Contracts;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BlugraryDetectionSystemBAL.Implementation.Cryptography
{
    public class AESEncryption : ICryptographyBAL
    {
        private AppSettings appSettings;
        public AESEncryption(AppSettings _appSettings)
        {
            appSettings = _appSettings;
        }

        public string EncryptData(string textData,out string privateKey)
        {
            string encryptedText = null;
            privateKey = appSettings.appKeys.aesPrivateKey;
            try
            {
                RijndaelManaged objrij = new RijndaelManaged();
                objrij.Mode = CipherMode.CBC;
                //set the padding mode used in the algorithm.
                objrij.Padding = PaddingMode.PKCS7;
                //set the size, in bits, for the secret key.
                objrij.KeySize = 0x80;
                //set the block size in bits for the cryptographic operation.
                objrij.BlockSize = 0x80;
                //set the symmetric key that is used for encryption & decryption.
                byte[] passBytes = Encoding.UTF8.GetBytes(privateKey);
                //set the initialization vector (IV) for the symmetric algorithm
                byte[] encryptionkeyBytes = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                int len = passBytes.Length;
                if (len > encryptionkeyBytes.Length)
                {
                    len = encryptionkeyBytes.Length;
                }
                Array.Copy(passBytes, encryptionkeyBytes, len);
                objrij.Key = encryptionkeyBytes;
                objrij.IV = encryptionkeyBytes;
                //Creates symmetric AES object with the current key and initialization vector IV.
                ICryptoTransform objtransform = objrij.CreateEncryptor();
                byte[] textDataByte = Encoding.UTF8.GetBytes(textData);
                //Final transform the test string.
                encryptedText = Convert.ToBase64String(objtransform.TransformFinalBlock(textDataByte, 0, textDataByte.Length));
            }
            catch (Exception ex)
            {
                throw;
            }
            return encryptedText;
        }

        public string DecryptData(string encryptedText,string privateKey)
        {
            string decryptedText = null;
            privateKey = appSettings.appKeys.aesPrivateKey;
            try
            {
                
                RijndaelManaged objrij = new RijndaelManaged();
                objrij.Mode = CipherMode.CBC;
                objrij.Padding = PaddingMode.PKCS7;
                objrij.KeySize = 0x80;
                objrij.BlockSize = 0x80;
                byte[] encryptedTextByte = Convert.FromBase64String(encryptedText);
                byte[] passBytes = Encoding.UTF8.GetBytes(privateKey);
                byte[] encryptionkeyBytes = new byte[0x10];
                int len = passBytes.Length;
                if (len > encryptionkeyBytes.Length)
                {
                    len = encryptionkeyBytes.Length;
                }
                Array.Copy(passBytes, encryptionkeyBytes, len);
                objrij.Key = encryptionkeyBytes;
                objrij.IV = encryptionkeyBytes;
                byte[] TextByte = objrij.CreateDecryptor().TransformFinalBlock(encryptedTextByte, 0, encryptedTextByte.Length);
                decryptedText = Encoding.UTF8.GetString(TextByte);  //it will return readable string
            }
            catch (Exception ex)
            {
                throw;
            }
            return decryptedText;
        }

    }
}
