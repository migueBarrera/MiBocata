using Mibocata.Core.Services.Interfaces;
using MiBocata.Framework;

namespace MiBocata.Services
{
    internal class AppSecretsService : IAppSecretsService
    {
        public string GetUrlBase()
        {
            return DefaultSettings.URL_BASE;
        }
    }
}
