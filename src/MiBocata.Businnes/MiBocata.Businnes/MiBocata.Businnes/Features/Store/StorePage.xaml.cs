using MiBocata.Businnes.Framework;
using Xamarin.Forms.Xaml;

namespace MiBocata.Businnes.Features.Store
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StorePage : BasePage
    {
        public StorePage()
        {
            InitializeComponent();

            BindingContext = Locator.Resolve<StoreViewModel>();
        }
    }
}