using Mibocata.Core.Services.Interfaces;

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
