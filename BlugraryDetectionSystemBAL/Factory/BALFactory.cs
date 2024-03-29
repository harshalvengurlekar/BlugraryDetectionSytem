﻿using BlugraryDetectionSystemApi.MiscClasses;
using BlugraryDetectionSystemBAL.Contracts;
using BlugraryDetectionSystemBAL.Implementation;
using BlugraryDetectionSystemBAL.Implementation.Cryptography;


namespace BlugraryDetectionSystemBAL.Factory
{
    public static class BALFactory
    {

        public static IUserBAL GetUserBALObj(AppSettings appSettings)
        {
            return new UserBAL(appSettings);
        }

        public static IResidentBAL GetResidentBALObj(AppSettings appSettings)
        {
            return new ResidentBAL(appSettings);
        }
        
        public static ICryptographyBAL GetRCF2898AlgorithmBALObj()
        {
            return new RCF2898AlgorithmBAL();
        }

        public static ICryptographyBAL GetSHA256BALObj()
        {
            return new SHA256Algorithm();
        }

        public static ICryptographyBAL GetAESAlgorithmBALObj(AppSettings appSettings)
        {
            return new AESEncryption(appSettings);
        }

    }
}
