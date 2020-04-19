using Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiBocata.Services.API.Interfaces
{
    public interface IStoreApi
    {
        [Post("/api/stores")]
        [Headers("Authorization: Bearer")]
        Task<Store> Create([Body] Store shopkeeper);
        
        [Get("/api/stores/{id}")]
        [Headers("Authorization: Bearer")]
        Task<Store> Get(int id);
        
        [Get("/api/stores")]
        [Headers("Authorization: Bearer")]
        Task<List<Store>> GetAll();
    }
}
