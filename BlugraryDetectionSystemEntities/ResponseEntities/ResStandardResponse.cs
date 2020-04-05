using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlugraryDetectionSystemEntities.ResponseEntities
{
    [Serializable]
    public class ResStandardResponse
    {
        [JsonProperty("message") ]
        public string Message { get; set; }

    }
}
