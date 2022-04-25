using Models.Core;
using System;

namespace Models.Responses
{
    public class ProductsResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; } = string.Empty;

        public string Image { get; set; } = string.Empty;

        public double UnitPrice { get; set; }

        public int StoreId { get; set; }

        public static Product Parse(ProductsResponse pr)
        {
            return new Product()
            {
                Id = pr.Id,
                Name = pr.Name,
                Description = pr.Description,
                Image = pr.Image,
                UnitPrice = pr.UnitPrice,
                StoreId = pr.StoreId,
            };
        }
    }
}
