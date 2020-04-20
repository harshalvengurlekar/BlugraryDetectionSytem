using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlugraryDetectionSystemEntities.RequestEntities
{
    [Serializable]
   public  class ReqGetUserResidents
    {
        [JsonProperty("userId")]
        [Required(ErrorMessage = "userId is required")]
        public string UserID { get; set; }
    }
}
