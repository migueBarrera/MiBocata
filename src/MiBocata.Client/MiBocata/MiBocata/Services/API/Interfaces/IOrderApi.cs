using Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiBocata.Services.API.Interfaces
{
    public interface IOrderApi
    {
        [Post("/api/orders")]
        [Headers("Authorization: Bearer")]
        Task<Order> Create([Body] Order order);

        [Get("/api/orders/{id}")]
        [Headers("Authorization: Bearer")]
        Task<Order> Get(int id);

        [Get("/api/orders")]
        [Headers("Authorization: Bearer")]
        Task<List<Order>> GetAll();
        
        [Get("/api/clients/{clientId}/orders")]
        [Headers("Authorization: Bearer")]
        Task<List<Order>> GetAllByClient(int clientId);
    }
}
