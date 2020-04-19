using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiBocata.Businnes.Services.Products
{
    public interface IProductsService
    {
        IEnumerable<Models.Product> GetProducts();
    }
}
