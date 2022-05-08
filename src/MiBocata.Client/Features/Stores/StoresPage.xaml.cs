using System.ComponentModel;

namespace MiBocata.Features.Stores;

public partial class StoresPage
{
    private bool isAlreadyInitialized;

    public StoresPage(StoresViewModel viewModel)
    {
        InitializeComponent();
        //MoveMapToSpain();

        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        ((StoresViewModel)BindingContext).PropertyChanged += OnCustomPropertyChanged;

        if (!isAlreadyInitialized)
        {
            base.OnAppearing();
            isAlreadyInitialized = true;
        }
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
    }

    //private void MoveMapToSpain()
    //{
    //    var position = new Position(37.3753501, -6.0250983);
    //    MyMap.MoveToRegion(new MapSpan(position, 0.01, 0.01));
    //}

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        ((StoresViewModel)BindingContext).PropertyChanged -= OnCustomPropertyChanged;
    }

    private void OnCustomPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        //if (e.PropertyName == nameof(StoresViewModel.UserLocation))
        //{
        //    var location = ((StoresViewModel)BindingContext).UserLocation;
        //    if (location != null)
        //    {
        //        MyMap.MyLocationEnabled = true;
        //        var position = new Position(location.Latitude, location.Longitude);
        //        MyMap.MoveToRegion(new MapSpan(position, 0.01, 0.01));
        //    }
        //}
    }

    //private void MyMap_PinClicked(object sender, PinClickedEventArgs e)
    //{
    //    var pin = e.Pin;

    //    // StorePopup.TranslateTo(0,120, ExpandAnimationSpeed, Easing.SinInOut);
    //}

    //private void MyMap_MapClicked(object sender, MapClickedEventArgs e)
    //{
    //}

    //private async void MyMap_InfoWindowClicked(object sender, InfoWindowClickedEventArgs e)
    //{
    //    var store = ((StoresViewModel)BindingContext).Stores.Where(x => x.Position == e.Pin.Position).FirstOrDefault();
    //    if (store != null)
    //    {
    //        await ((StoresViewModel)BindingContext).GoToStore(store);
    //    }
    //}
}