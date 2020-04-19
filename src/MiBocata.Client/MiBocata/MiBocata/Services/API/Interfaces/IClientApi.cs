using Models;
using Refit;
using System.Threading.Tasks;

namespace MiBocata.Services.API.Interfaces
{
    public interface IClientApi
    {
        [Put("/api/clients/{id}")]
        Task UploadClient(int id, [Body] Client client);
        
        [Post("/api/clients/{id}/image")]
        Task<string> UploadPhoto(int id, [AliasAs("file")] StreamPart stream);
    }
}
