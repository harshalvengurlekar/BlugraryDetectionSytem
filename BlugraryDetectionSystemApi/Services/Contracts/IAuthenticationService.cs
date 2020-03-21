using BlugraryDetectionSystemEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlugraryDetectionSystemApi.Services.Contracts
{
    public interface IUserAuthenticationService
    {
        User Authenticate(string username, string password);
    }
}
