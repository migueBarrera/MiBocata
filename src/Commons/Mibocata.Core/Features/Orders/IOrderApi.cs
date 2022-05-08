using Models.Requests;
using Models.Responses;
using Refit;

namespace Mibocata.Core.Features.Orders
{
    public interface IOrderApi
    {
        [Get("/api/stores/{storeId}/orders")]
        [Headers("Authorization: Bearer")]
        Task<List<OrdersResponse>> GetAllByStore(int storeId);

        [Put("/api/orders/{id}")]
        [Headers("Authorization: Bearer")]
        Task<HttpResponseMessage> Update(int id, UpdateOrderRequest resquest);

        [Post("/api/orders")]
        [Headers("Authorization: Bearer")]
        Task<OrdersResponse> Create([Body] CreateOrderRequest request);

        [Get("/api/orders/{id}")]
        [Headers("Authorization: Bearer")]
        Task<OrdersResponse> Get(int id);

        [Get("/api/orders")]
        [Headers("Authorization: Bearer")]
        Task<List<OrdersResponse>> GetAll();

        [Get("/api/clients/{clientId}/orders")]
        [Headers("Authorization: Bearer")]
        Task<List<OrdersResponse>> GetAllByClient(int clientId);
    }
}
