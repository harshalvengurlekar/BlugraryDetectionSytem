using Newtonsoft.Json;
using System;


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
        public string  authenticationPrivateKey { get; set; }

        [JsonProperty("dbConnectionString")]
        public string dbConnectionString { get; set; }
    }
}
