using Newtonsoft.Json;

namespace Models
{
    public class Product
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; } = string.Empty;
        
        [JsonProperty(PropertyName = "image")]
        public string Image { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "unitPrice")]
        public double UnitPrice { get; set; }

        [JsonProperty(PropertyName = "storeId")]
        public int StoreId { get; set; }
    }
}
