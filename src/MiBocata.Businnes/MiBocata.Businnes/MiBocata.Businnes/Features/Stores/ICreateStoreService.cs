using Models.Core;
using System.Threading.Tasks;

namespace MiBocata.Businnes.Features.Stores
{
    public interface ICreateStoreService
    {
        Task CreateStore(Store store);
    }
}
