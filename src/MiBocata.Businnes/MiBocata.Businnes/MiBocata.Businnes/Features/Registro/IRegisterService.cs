using Models.Core;
using System.Threading.Tasks;

namespace MiBocata.Businnes.Features.Registro
{
    public interface IRegisterService
    {
        Task RegisterCommandAsync(Shopkeeper newShopkeeper);
    }
}
