using Xamarin.Essentials;

namespace MiBocata.Businnes.Services.ConnectivityServices
{
    public class ConnectivityService : IConnectivityService
    {
        public bool IsThereInternet => Connectivity.NetworkAccess == NetworkAccess.Internet;
    }
}
