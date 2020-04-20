using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlugraryDetectionSystemEntities
{
    [Serializable]
    public class ResAuthToken
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("userid")]
        public string UserID { get; set; }
    }


}
