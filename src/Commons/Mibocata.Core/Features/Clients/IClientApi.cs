using Models.Requests;
using Refit;

namespace Mibocata.Core.Features.Clients
{
    public interface IClientApi
    {
        [Put("/api/clients/{id}")]
        Task UploadClient(int id, [Body] ClientUpdateRequest request);

        [Post("/api/clients/{id}/image")]
        Task<string> UploadPhoto(int id, [AliasAs("file")] StreamPart stream);
    }
}
