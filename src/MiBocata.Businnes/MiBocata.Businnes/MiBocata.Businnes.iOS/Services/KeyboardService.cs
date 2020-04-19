using System;
using MiBocata.Businnes.iOS.Services;
using MiBocata.Businnes.Services.KeyboardService;
using Xamarin.Forms;

[assembly: Dependency(typeof(KeyboardService))]
namespace MiBocata.Businnes.iOS.Services
{
    public class KeyboardService : IKeyboardService
    {
        public void HideSoftKeyboard()
        {
            throw new NotImplementedException();
        }
    }
}