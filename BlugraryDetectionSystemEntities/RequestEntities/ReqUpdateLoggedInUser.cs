using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlugraryDetectionSystemEntities.RequestEntities
{
    [Serializable]
    public class ReqUpdateLoggedInUserInfo
    {
        [JsonProperty("userName")]
        [Required(ErrorMessage ="userName is required")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        [Required(ErrorMessage = "password is required")]
        public string Password { get; set; }

        [JsonProperty("name")]
        [Required(ErrorMessage = "name is required")]
        public string Name { get; set; }

        [JsonProperty("age")]
        [Required(ErrorMessage = "age is required")]
        public string Age { get; set; }

        [JsonProperty("email")]
        [Required(ErrorMessage = "email is required")]
        public string Email { get; set; }

        [JsonProperty("userId")]
        [Required(ErrorMessage = "userID is required")]
        public string UserID { get; set; }


        private string Salt { get; set; }

        public void SetSlat(string Salt)
        {
            this.Salt = Salt;
        }

        public string GetSalt()
        {
            return this.Salt;
        }

    }
}
