using MiBocata.Businnes.Features.Stores;
using MiBocata.Businnes.Framework;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace MiBocata.Businnes.Features.Stores
{
    public partial class CreateStorePage : BasePage
    {
        public CreateStorePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            ((CreateStoreViewModel)BindingContext).PropertyChanged += OnCustomPropertyChanged;
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ((CreateStoreViewModel)BindingContext).PropertyChanged -= OnCustomPropertyChanged;
        }

        private void OnCustomPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CreateStoreViewModel.Store))
            {
                var location = ((CreateStoreViewModel)BindingContext).Store.StoreLocation;
                if (location != null)
                {
                    // MyMap.IsShowingUser = true;
                    var position = new Xamarin.Forms.Maps.Position(location.Latitude, location.Longitude);
                    MyMap.MoveToRegion(
                        MapSpan.FromCenterAndRadius(
                            position, Distance.FromMeters(30)));
                }
            }
        }
    }
}