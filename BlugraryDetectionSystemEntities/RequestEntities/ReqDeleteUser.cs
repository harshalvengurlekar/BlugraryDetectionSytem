using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlugraryDetectionSystemEntities.RequestEntities
{
    [Serializable]
    public class ReqDeleteUser
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }
    }
}
