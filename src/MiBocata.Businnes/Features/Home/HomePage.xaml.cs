using Mibocata.Core.Framework;

namespace MiBocata.Businnes.Features.Home;

public partial class HomePage : TabbedPage
{
    public HomePage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        ((CoreViewModel)BindingContext)?.OnAppearingAsync();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        ((CoreViewModel)BindingContext)?.OnDisappearingAsync();
    }
}