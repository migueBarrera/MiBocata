using Refit;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MiBocata.Businnes.Services.API.Interfaces
{
    public interface IOrderApi
    {
        [Get("/api/stores/{storeId}/orders")]
        [Headers("Authorization: Bearer")]
        Task<List<Models.Order>> GetAllByStore(int storeId);
        
        [Put("/api/orders/{id}")]
        [Headers("Authorization: Bearer")]
        Task<HttpResponseMessage> Update(int id, Models.Order order);
    }
}
