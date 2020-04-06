using BlugraryDetectionSystemApi.MiscClasses;
using BlugraryDetectionSystemApi.Services.Contracts;
using BlugraryDetectionSystemBAL.Contracts;
using BlugraryDetectionSystemBAL.Factory;
using BlugraryDetectionSystemEntities;
using BlugraryDetectionSystemEntities.RequestEntities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace BlugraryDetectionSystemApi.Services.Implementations
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private AppSettings appSettings { get; set; }
        private IUserBAL userBAL;
        //Constructor for Injection of Appsettings
        public UserAuthenticationService(IOptions<AppSettings> _appSettings)
        {
            this.appSettings = _appSettings.Value;
            this.userBAL = BALFactory.GetUserBALObj(appSettings);
        }


        public ResAuthToken Authenticate(ReqUserAuth reqUserAuth)
        {
            ResAuthToken authToken = null;
            try
            {
                string userId = "";
                bool isAuthenticate = userBAL.AuthenticateUser(reqUserAuth,ref userId);
                // return null if user not found
                if(isAuthenticate)
                {
                    authToken = new ResAuthToken();
                    authToken.UserName = reqUserAuth.UserName;
                    authToken.UserID = userId;
                    // authentication successful so generate jwt token
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(appSettings.appKeys.authenticationPrivateKey);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                    new Claim(ClaimTypes.Name, authToken.UserName.ToString())
                        }),
                        Expires = DateTime.UtcNow.AddDays(7),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    authToken.Token = tokenHandler.WriteToken(token);
                }

               
            }
            catch(Exception ex)
            {
                throw;
            }
            return authToken;
        }

    }
}
