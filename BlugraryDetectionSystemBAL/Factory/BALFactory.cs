using BlugraryDetectionSystemApi.MiscClasses;
using BlugraryDetectionSystemBAL.Contracts;
using BlugraryDetectionSystemBAL.Implementation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlugraryDetectionSystemBAL.Factory
{
    public static class BALFactory
    {

        public static IUserBAL GetUserBALObj(AppSettings appSettings)
        {
            return new UserBAL(appSettings);
        }

    }
}
