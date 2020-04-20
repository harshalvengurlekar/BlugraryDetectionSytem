using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlugraryDetectionSystemEntities.RequestEntities
{
    [Serializable]
    public class ReqUpdateUser
    {
        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("age")]
        public string Age { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("userType")]
        public string UserType { get; set; }

    }
}
