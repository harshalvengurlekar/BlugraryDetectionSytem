using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlugraryDetectionSystemApi.MiscClasses
{
    [Serializable]
    public class AppSettings
    {
        [JsonProperty("appKeys")]
        public AppKeys appKeys { get; set; }
    }

    [Serializable]
    public class AppKeys
    {
        [JsonProperty("privateKey")]
        public string privateKey { get; set; }
    }
}
