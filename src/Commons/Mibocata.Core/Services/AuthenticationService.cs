using Mibocata.Core.Services.Interfaces;

namespace MiBocata.Businnes.Services.Commons.AuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        public AuthenticationService()
        {
        }

        public bool IsLoggedIn => Preferences.Get("key_shopkeeper", string.Empty) != string.Empty;
    }
}
