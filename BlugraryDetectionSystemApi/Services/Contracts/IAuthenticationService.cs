using BlugraryDetectionSystemEntities;
using BlugraryDetectionSystemEntities.RequestEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlugraryDetectionSystemApi.Services.Contracts
{
    public interface IUserAuthenticationService
    {
        ResAuthToken Authenticate(ReqUserAuth reqUserAuth);
    }
}
