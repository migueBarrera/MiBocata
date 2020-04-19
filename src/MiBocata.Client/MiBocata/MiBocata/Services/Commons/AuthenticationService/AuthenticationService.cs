using System;

namespace MiBocata.Services.AuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        public bool IsLoggedIn => Xamarin.Essentials.Preferences.Get("key_client", string.Empty) != string.Empty;
    }
}
