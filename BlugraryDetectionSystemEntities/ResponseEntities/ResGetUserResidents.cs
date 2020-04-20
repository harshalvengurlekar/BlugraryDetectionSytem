using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlugraryDetectionSystemEntities.ResponseEntities
{
    [Serializable]
    public class ResGetUserResidents
    {
        [JsonProperty("residentName")]
        public string ResidentName { get; set; }

        [JsonProperty("residentPhoto")]
        public string ResidentPhotos { get; set; }
    }
}
