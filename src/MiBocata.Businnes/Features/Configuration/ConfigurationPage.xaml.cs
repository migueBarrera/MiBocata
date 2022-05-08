namespace MiBocata.Businnes.Features.Configuration;

public partial class ConfigurationPage
{
    public ConfigurationPage()
    {
        InitializeComponent();
    }

    //protected override void OnAppearing()
    //{
    //    ((ConfigurationViewModel)BindingContext).PropertyChanged += OnCustomPropertyChanged;
    //    base.OnAppearing();
    //}

    //protected override void OnDisappearing()
    //{
    //    base.OnDisappearing();

    //    ((ConfigurationViewModel)BindingContext).PropertyChanged -= OnCustomPropertyChanged;
    //}

    //private void OnCustomPropertyChanged(object sender, PropertyChangedEventArgs e)
    //{
    //    if (e.PropertyName == nameof(ConfigurationViewModel.Store))
    //    {
    //        var location = ((ConfigurationViewModel)BindingContext).Store.StoreLocation;
    //        if (location != null)
    //        {
    //            // MyMap.IsShowingUser = true;
    //            var position = new Xamarin.Forms.Maps.Position(location.Latitude, location.Longitude);
    //            MyMap.MoveToRegion(
    //                MapSpan.FromCenterAndRadius(
    //                    position, Distance.FromMeters(30)));
    //        }
    //    }
    //}
}