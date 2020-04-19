using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MiBocata.Businnes.Services.Preferences;
using Models;

namespace MiBocata.Businnes.Services.Products
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
            return preferencesService.GetStore().Products;
        }
    }
}
