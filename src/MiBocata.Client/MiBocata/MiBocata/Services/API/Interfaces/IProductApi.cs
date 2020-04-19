using Models;
using Refit;
using System.Threading.Tasks;

namespace MiBocata.Services.API.Interfaces
{
    public interface IProductApi
    {
        [Post("/api/products")]
        [Headers("Authorization: Bearer")]
        Task<Product> Create([Body] Product product);
    }
}
