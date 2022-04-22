using Models.Requests;
using Models.Responses;
using Refit;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mibocata.Core.Features.Stores
{
    public interface IStoreApi
    {
        [Post("/api/stores")]
        [Headers("Authorization: Bearer")]
        Task<StoreResponse> Create([Body] StoreCreateRequest request);

        [Get("/api/stores/{id}")]
        [Headers("Authorization: Bearer")]
        Task<StoreResponse> Get(int id);

        [Put("/api/stores/{id}")]
        [Headers("Authorization: Bearer")]
        Task<HttpResponseMessage> Update(int id, [Body] StoreUpdateRequest request);

        [Get("/api/stores")]
        [Headers("Authorization: Bearer")]
        Task<IEnumerable<StoreResponse>> GetAll();
    }
}
