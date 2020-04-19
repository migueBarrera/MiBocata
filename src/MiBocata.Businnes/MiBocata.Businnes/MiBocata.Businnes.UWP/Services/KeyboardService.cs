using MiBocata.Businnes.Services.KeyboardService;
using MiBocata.Businnes.UWP.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(KeyboardService))]

namespace MiBocata.Businnes.UWP.Services
{
    public class KeyboardService : IKeyboardService
    {
        public void HideSoftKeyboard()
        {
            System.Console.WriteLine($"{nameof(KeyboardService)} not work in UWP");
        }
    }
}
