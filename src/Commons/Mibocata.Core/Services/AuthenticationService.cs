using Mibocata.Core.Services.Interfaces;

namespace Mibocata.Core.Services;

public class AuthenticationService : IAuthenticationService
{
    public AuthenticationService()
    {
    }

    public bool IsLoggedIn => Preferences.Get("key_shopkeeper", string.Empty) != string.Empty;
}
