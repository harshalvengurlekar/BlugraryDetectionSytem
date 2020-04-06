using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlugraryDetectionSystemEntities.RequestEntities
{
    [Serializable]
    public class ReqAddResidents
    {
        [JsonProperty("residentName")]
        [Required(ErrorMessage = "residentName is required")]
        public string ResidentName { get; set; }

        [JsonProperty("userId")]
        [Required(ErrorMessage = "userId is required")]
        public string UserId { get; set; }

        [JsonProperty("fileBase64")]
        [Required(ErrorMessage = "fileBase64 is required")]
        public string FileBase64 { get; set; }


        private string FilePath { get; set; }


        public void SetFilePath(string filePath)
        {
            this.FilePath = filePath;
        }

        public string GetFilePath()
        {
            return this.FilePath;
        }

    }
}
