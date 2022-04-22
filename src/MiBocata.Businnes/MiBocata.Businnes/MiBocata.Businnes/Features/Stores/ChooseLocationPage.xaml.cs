using System.ComponentModel;
using MiBocata.Businnes.Features.Stores;
using MiBocata.Businnes.Framework;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace MiBocata.Businnes.Features.Stores
{
    public partial class ChooseLocationPage : BasePage
    {
        public ChooseLocationPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            ((ChooseLocationViewModel)BindingContext).PropertyChanged += OnCustomPropertyChanged;
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ((ChooseLocationViewModel)BindingContext).PropertyChanged -= OnCustomPropertyChanged;
        }

        private void OnCustomPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ChooseLocationViewModel.UserLocation))
            {
                var location = ((ChooseLocationViewModel)BindingContext).UserLocation;
                if (location != null)
                {
                    MyMap.IsShowingUser = true;
                    var position = new Xamarin.Forms.Maps.Position(location.Latitude, location.Longitude);
                    MyMap.MoveToRegion(new MapSpan(position, 0.01, 0.01));
                }
            }
        }

        private void MyMap_MapClicked(object sender, MapClickedEventArgs e)
        {
            if (e.Position == null)
            {
                return;
            }

            ((ChooseLocationViewModel)BindingContext).UserSelectedPosition(e.Position);
        }
    }
}