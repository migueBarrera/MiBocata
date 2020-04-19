using Newtonsoft.Json;

namespace Models
{
    public class OrderProduct
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "image")]
        public string Image { get; set; } = string.Empty;
        
        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "unitPrice")]
        public double UnitPrice { get; set; }

        [JsonProperty(PropertyName = "quantity")]
        public int Quantity { get; set; }

        [JsonProperty(PropertyName = "orderId")]
        public int OrderId { get; set; }

        [JsonProperty(PropertyName = "idOriginalProduct")]
        public int IdOriginalProduct { get; set; }
    }
}