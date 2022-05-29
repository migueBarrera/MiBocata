using Mibocata.Core.Framework;
using Models.Core;

namespace MiBocata.Businnes.Features.Registro
{
    public interface IRegisterService
    {
        Task RegisterCommandAsync(IBusyViewModel viewModel, Shopkeeper newShopkeeper);
    }
}
