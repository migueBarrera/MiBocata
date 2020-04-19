using Xamarin.Essentials;

namespace MiBocata.Services.ConnectivityServices
{
    public class ConnectivityService : IConnectivityService
    {
        public bool IsThereInternet => Connectivity.NetworkAccess == NetworkAccess.Internet;
    }
}
