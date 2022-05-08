using Models.Requests;
using Models.Responses;
using Refit;

namespace Mibocata.Core.Features.Products
{
    public interface IProductApi
    {
        [Post("/api/products")]
        [Headers("Authorization: Bearer")]
        Task<CreateProductResponse> Create([Body] CreateProductRequest product);

        [Get("/api/stores/{id}/products")]
        [Headers("Authorization: Bearer")]
        Task<List<ProductsResponse>> GetByStore(int id);

        [Multipart]
        [Post("/api/products/{id}/image")]
        [Headers("Authorization: Bearer")]
        Task UploadPhoto(int id, [AliasAs("file")] StreamPart stream);
    }
}
