﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace BlugraryDetectionSystemEntities
{
    [Serializable]
    public class ReqRegisterUser
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

        [JsonProperty("roleId")]
        public int RoleId { get; set; } = 2;

        private string Salt { get; set; }


        public void SetSalt(string salt)
        {
            Salt = salt;
        }

        public string GetSalt()
        {
            return Salt;
        }
    }
}
