using MiBocata.Framework;
using Xamarin.Forms;

namespace MiBocata.Features.LogIn
{
    public partial class LogInControlView : ContentView
    {
        public LogInControlView()
        {
            InitializeComponent();

            BindingContext = Locator.Resolve<LogInControlViewModel>();
        }
    }
}