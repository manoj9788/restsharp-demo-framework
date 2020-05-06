using System;
using Newtonsoft.Json;

namespace RestSharpDemo.Model
{
    public class Users
    {
        //Request Data
        [JsonProperty("name")]
        public string name { get; set; }
        
        [JsonProperty("job")]
        public string job { get; set; }
        
        [JsonProperty("email")]
        public string email { get; set; }
        
        [JsonProperty("password")]
        public string password { get; set; }
        
        //Response Data
        public string id { get; set; }
        public string token { get; set; }

    }
}