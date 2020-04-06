using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlugraryDetectionSystemEntities.RequestEntities
{
    [Serializable]
   public  class ReqGetResidents
    {
        [JsonProperty("userId")]
        public string UserID { get; set; }
    }
}
