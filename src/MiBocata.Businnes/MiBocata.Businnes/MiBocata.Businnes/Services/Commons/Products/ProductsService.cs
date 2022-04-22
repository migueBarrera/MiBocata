using System.Collections.Generic;
using System.Linq;
using MiBocata.Businnes.Services.Commons.Preferences;
using Models.Core;

namespace MiBocata.Businnes.Services.Commons.Products
{
    public class ProductsService : IProductsService
    {
        private readonly IPreferencesService preferencesService;

        public ProductsService(IPreferencesService preferencesService)
        {
            this.preferencesService = preferencesService;
        }

        public IEnumerable<Product> GetProducts()
        {
            //TODO map to produc, o no ?

            var productResponse = preferencesService.GetStore().Products;
            return productResponse.Select((x) => new Product());
        }
    }
}
