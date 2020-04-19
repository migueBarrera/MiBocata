using MiBocata.iOS.Services;
using MiBocata.Services.KeyboardService;
using System;
using Xamarin.Forms;

[assembly: Dependency(typeof(KeyboardService))]

namespace MiBocata.iOS.Services
{
    public class KeyboardService : IKeyboardService
    {
        public void HideSoftKeyboard()
        {
            throw new NotImplementedException();
        }
    }
}