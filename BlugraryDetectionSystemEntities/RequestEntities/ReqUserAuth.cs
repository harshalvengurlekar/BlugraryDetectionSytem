using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlugraryDetectionSystemEntities.RequestEntities
{
    [Serializable]
    public class ReqUserAuth
    {
        [JsonProperty("username")]
        [Required(ErrorMessage = "username is required")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        [Required(ErrorMessage = "password is required")]
        public string Password { get; set; }



    }
}
