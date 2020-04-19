using Newtonsoft.Json;
using System.Collections.Generic;

namespace Models
{

    public class StoreLocation
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "latitude")]
        public double Latitude { get; set; }

        [JsonProperty(PropertyName = "longitude")]
        public double Longitude { get; set; }

        [JsonProperty(PropertyName = "countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty(PropertyName = "CountryName")]
        public string CountryName { get; set; }

        [JsonProperty(PropertyName = "FeatureName")]
        public string FeatureName { get; set; }

        [JsonProperty(PropertyName = "PostalCode")]
        public string PostalCode { get; set; }

        [JsonProperty(PropertyName = "SubLocality")]
        public string SubLocality { get; set; }

        [JsonProperty(PropertyName = "Thoroughfare")]
        public string Thoroughfare { get; set; }

        [JsonProperty(PropertyName = "SubThoroughfare")]
        public string SubThoroughfare { get; set; }

        [JsonProperty(PropertyName = "Locality")]
        public string Locality { get; set; }

        [JsonProperty(PropertyName = "AdminArea")]
        public string AdminArea { get; set; }

        [JsonProperty(PropertyName = "SubAdminArea")]
        public string SubAdminArea { get; set; }

        [JsonProperty(PropertyName = "StoreId")]
        public int StoreId { get; set; }
    }
}