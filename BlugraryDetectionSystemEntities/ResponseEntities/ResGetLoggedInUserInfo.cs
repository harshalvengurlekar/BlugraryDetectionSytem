using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlugraryDetectionSystemEntities.ResponseEntities
{
    [Serializable]
    public class ResGetLoggedInUserInfo
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("age")]
        public string Age { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("userId")]
        public string UserID { get; set; }
    }
}
