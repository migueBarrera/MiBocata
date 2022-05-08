using MiBocata.Businnes.Framework;
using Mibocata.Core.Services.Interfaces;

namespace MiBocata.Businnes.Services
{
    internal class AppSecretsService : IAppSecretsService
    {
        public string GetUrlBase()
        {
            return DefaultSettings.URL_BASE;
        }
    }
}
