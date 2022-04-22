using Mibocata.Core.Extensions;
using MiBocata.Businnes.Features.Stores;
using MiBocata.Businnes.Framework;
using Xamarin.Forms.Xaml;

namespace MiBocata.Businnes.Features.Stores
{
    public partial class StorePage : BasePage
    {
        public StorePage()
        {
            InitializeComponent();

            BindingContext = App.Current.DependencyService.Resolve<StoreViewModel>();
        }
    }
}