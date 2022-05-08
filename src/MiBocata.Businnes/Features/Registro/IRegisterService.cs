using Models.Core;

namespace MiBocata.Businnes.Features.Registro
{
    public interface IRegisterService
    {
        Task RegisterCommandAsync(Shopkeeper newShopkeeper);
    }
}
