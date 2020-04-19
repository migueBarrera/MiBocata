using Newtonsoft.Json;
using System.Collections.Generic;

namespace Models
{
    public class Store
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "image")]
        public string Image { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "products")]
        public List<Product> Products { get; set; }

        [JsonProperty(PropertyName = "autoAccept")]
        public bool AutoAccept { get; set; }

        [JsonProperty(PropertyName = "storeLocation")]
        public StoreLocation StoreLocation { get; set; }
    }
}