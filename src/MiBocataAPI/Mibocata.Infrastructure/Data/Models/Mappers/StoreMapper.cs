using Models.Requests;
using Models.Responses;
using System;

namespace Mibocata.Infrastructure.Data.Models.Mappers
{
    public static class StoreMapper
    {
        public static ProductsResponse Parse(Product product)
        {
            return new ProductsResponse()
            {
                Id = product.Id,
                Name = product.Name,
                Image = product.Image,
                Description = product.Description,
                StoreId = product.StoreId,
                UnitPrice = product.UnitPrice,
            };
        }

        public static StoreResponse Parse(Store store)
        {
            return new StoreResponse()
            {
                Id = store.Id,
                Name = store.Name,
                Image = store.Image,
                AutoAccept = store.AutoAccept,
                //TODO review
                //Products = store.Products,
                //StoreLocation = store.StoreLocation,
            };
        }

        public static Store Parse(StoreCreateRequest request)
        {
            return new Store()
            {
                Id = request.Id,
                Name = request.Name,
                Image = request.Image,
                AutoAccept = request.AutoAccept,
                //TODO review
                //Products = request.Products,
                //StoreLocation = request.StoreLocation,
            };
        }
    }
}
