using Models.Core;
using System.Collections.Generic;

namespace MiBocata.Businnes.Services.Commons.Products
{
    public interface IProductsService
    {
        IEnumerable<Product> GetProducts();
    }
}
