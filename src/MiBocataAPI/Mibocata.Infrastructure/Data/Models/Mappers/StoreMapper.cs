using Models.Requests;
using Models.Responses;
using System.Linq;

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
                Products = store.Products?.Select(p => StoreMapper.Parse(p)).ToList() ?? new System.Collections.Generic.List<ProductsResponse>(),
                StoreLocation = StoreLocation.Parse(store.StoreLocation),
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
                //TODO review => Creo que no se necesita en este request
                //Products = request.Products,
                StoreLocation = StoreLocation.Parse(request.StoreLocation ?? new StoreLocationRequest()),
            };
        }
    }
}
