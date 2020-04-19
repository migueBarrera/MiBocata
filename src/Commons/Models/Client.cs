using Newtonsoft.Json;
using System;

namespace Models
{
    public class Client
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        ////[JsonProperty(PropertyName = "pushToken")]
        ////public string PushToken { get; set; }

        [JsonProperty(PropertyName = "image")]
        public string Image { get; set; } = string.Empty;
        
        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }
        
        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }
    }
}
