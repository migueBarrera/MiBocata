using Models;
using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace MiBocata.Businnes.Services.API.Interfaces
{
    public interface IStoreApi
    {
        [Post("/api/stores")]
        [Headers("Authorization: Bearer")]
        Task<Store> Create([Body] Store shopkeeper);
        
        [Get("/api/stores/{id}")]
        [Headers("Authorization: Bearer")]
        Task<Store> Get(int id);

        [Put("/api/stores/{id}")]
        [Headers("Authorization: Bearer")]
        Task<HttpResponseMessage> Update(int id, [Body]Store store);
    }
}
