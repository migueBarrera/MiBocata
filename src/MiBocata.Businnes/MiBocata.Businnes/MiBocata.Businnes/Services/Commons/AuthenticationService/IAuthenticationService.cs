using System;
using System.Collections.Generic;
using System.Text;

namespace MiBocata.Businnes.Services.AuthenticationService
{
    public interface IAuthenticationService
    {
        bool IsLoggedIn { get; }
    }
}
