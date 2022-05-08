using Models.Core;

namespace MiBocata.Businnes.Features.Stores
{
    public interface ICreateStoreService
    {
        Task CreateStore(Store store);
    }
}
