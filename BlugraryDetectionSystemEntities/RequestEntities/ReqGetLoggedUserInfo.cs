using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlugraryDetectionSystemEntities.RequestEntities
{
    [Serializable]
    public class ReqGetLoggedUserInfo
    {
        [Required(ErrorMessage ="userName is required")]
        [JsonProperty("userName")]
        public string UserName { get; set; }
    }
}
