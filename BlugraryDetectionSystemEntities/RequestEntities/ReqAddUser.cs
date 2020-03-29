using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlugraryDetectionSystemEntities
{
    [Serializable]
    public class ReqAddUser
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty("roleId")]
        public int RoleId { get; set; }
    }
}
