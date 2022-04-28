using Models.Core;
using System.Collections.Generic;
using System.Linq;

namespace Models.Responses
{
    public class StoreResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Image { get; set; } = string.Empty;

        public List<ProductsResponse> Products { get; set; }

        public bool AutoAccept { get; set; }

        public StoreLocationResponse StoreLocation { get; set; }

        public static Store Parse(StoreResponse response)
        {
            return new Store()
            {
                Id = response.Id,
                Name = response.Name,
                Image = response.Image,
                AutoAccept = response.AutoAccept,
                StoreLocation = response.StoreLocation != null
                        ? StoreLocationResponse.Parse(response.StoreLocation)
                        : null,
                Products = response.Products?.Select(pr => ProductsResponse.Parse(pr)).ToList() ?? new System.Collections.Generic.List<Product>(),
            };
        }
    }
}
