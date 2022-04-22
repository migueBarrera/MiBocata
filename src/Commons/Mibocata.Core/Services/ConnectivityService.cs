using Mibocata.Core.Services.Interfaces;
using Xamarin.Essentials;

namespace Mibocata.Core.Services
{
    public class ConnectivityService : IConnectivityService
    {
        public bool IsThereInternet => Connectivity.NetworkAccess == NetworkAccess.Internet;
    }
}
