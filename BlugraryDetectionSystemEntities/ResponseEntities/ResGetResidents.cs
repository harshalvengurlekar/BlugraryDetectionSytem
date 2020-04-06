using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlugraryDetectionSystemEntities.ResponseEntities
{
    [Serializable]
    public class ResGetResidents
    {
        [JsonProperty("residentName")]
        public string ResidentName { get; set; }

        [JsonProperty("residentPhoto")]
        public string ResidentPhotos { get; set; }
    }
}
