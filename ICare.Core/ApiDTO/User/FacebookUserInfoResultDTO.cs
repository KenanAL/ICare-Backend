﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICare.Core.ApiDTO
{
    public class FacebookUserInfoResultDTO
    {

        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("picture")]
        public FacebookPicture Picture { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
    }
    public class FacebookPicture
    {
        [JsonProperty("date")]
        public FacebookPictureData Date { get; set; }
    }
    public class FacebookPictureData
    {
        [JsonProperty("height")]
        public long Height { get; set; }
        [JsonProperty("is_silhouette")]
        public long IsSilhouette { get; set; }
        [JsonProperty("url")]
        public Uri Url { get; set; }
        [JsonProperty("width")]
        public long Width { get; set; }
    }
}
