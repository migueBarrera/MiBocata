using MiBocata.Businnes.Framework;
using MiBocata.Businnes.Services.Preferences;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiBocata.Businnes.Services.AuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        public AuthenticationService()
        {
        }

        public bool IsLoggedIn => Xamarin.Essentials.Preferences.Get("key_shopkeeper", string.Empty) != string.Empty;
    }
}
