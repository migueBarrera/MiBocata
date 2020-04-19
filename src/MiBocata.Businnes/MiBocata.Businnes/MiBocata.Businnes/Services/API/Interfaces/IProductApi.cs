using Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiBocata.Businnes.Services.API.Interfaces
{
    public interface IProductApi
    {
        [Post("/api/products")]
        [Headers("Authorization: Bearer")]
        Task<Product> Create([Body] Product product);
        
        [Get("/api/stores/{id}/products")]
        [Headers("Authorization: Bearer")]
        Task<List<Product>> GetByStore(int id);

        [Multipart]
        [Post("/api/products/{id}/image")]
        [Headers("Authorization: Bearer")]
        Task UploadPhoto(int id, [AliasAs("file")] StreamPart stream);
    }
}
