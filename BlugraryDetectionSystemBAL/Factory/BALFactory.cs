using BlugraryDetectionSystemApi.MiscClasses;
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

        
        public static ICryptographyBAL GetRCF2898AlgorithmBALObj()
        {
            return new RCF2898AlgorithmBAL();
        }

        public static ICryptographyBAL GetSHA256BALObj()
        {
            return new RCF2898AlgorithmBAL();
        }

    }
}
