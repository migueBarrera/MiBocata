using Models.Requests;
using Models.Responses;
using System;

namespace Mibocata.Infrastructure.Data.Models.Mappers
{
    public static class ProductMapper
    {
        public static ProductsResponse Parse(Product product)
        {
            return new ProductsResponse()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Image = product.Image,
                StoreId = product.StoreId,
                UnitPrice = product.UnitPrice,
            };
        }

        public static Product Parse(ProductCreateRequest request)
        {
            return new Product()
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                Image = request.Image,
                StoreId = request.StoreId,
                UnitPrice = request.UnitPrice,
            };
        }
    }
}
