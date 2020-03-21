using BlugraryDetectionSystemApi.MiscClasses;
using BlugraryDetectionSystemApi.Services.Contracts;
using BlugraryDetectionSystemEntities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlugraryDetectionSystemApi.Services.Implementations
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private AppSettings appSettings { get; set; }
        //Constructor for Injection of Appsettings
        public UserAuthenticationService(IOptions<AppSettings> _appSettings)
        {
            this.appSettings = _appSettings.Value;
        }


        public User Authenticate(string username, string password)
        {


            User user = new User();
            
            // return null if user not found


            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.appKeys.privateKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.password = null;

            return user;
        }

    }
}
