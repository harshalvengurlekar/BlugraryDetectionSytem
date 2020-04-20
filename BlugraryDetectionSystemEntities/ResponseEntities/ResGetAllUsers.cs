using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlugraryDetectionSystemEntities.ResponseEntities
{
    [Serializable]
    public class ResAllUsers
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("userID")]
        public string UserID { get; set; }

        [JsonProperty("age")]
        public string Age { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("roleName")]
        public string RoleName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
