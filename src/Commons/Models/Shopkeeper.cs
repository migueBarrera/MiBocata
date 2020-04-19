using Newtonsoft.Json;

namespace Models
{
    public class Shopkeeper
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
        
        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }

        [JsonProperty(PropertyName = "idStore")]
        public int IdStore { get; set; }
    }
}
