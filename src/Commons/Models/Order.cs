using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Models
{
    public class Order
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public double Amount { get; set; }

        [JsonProperty(PropertyName = "state")]
        public OrderStates State { get; set; } = OrderStates.DEFAULT;

        [JsonProperty(PropertyName = "pickupTime")]
        public DateTime PickupTime { get; set; }

        [JsonProperty(PropertyName = "created")]
        public DateTime Created { get; set; }

        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "clientId")]
        public int ClientId { get; set; }
        
        [JsonProperty(PropertyName = "client")]
        public Client Client { get; set; }
        
        [JsonProperty(PropertyName = "storeId")]
        public int StoreId { get; set; }

        [JsonProperty(PropertyName = "orderProducts")]
        public IEnumerable<OrderProduct> OrderProducts { get; set; }
        
        [JsonProperty(PropertyName = "store")]
        public Store Store { get; set; }
    }
}
