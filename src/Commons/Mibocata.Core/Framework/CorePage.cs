namespace Mibocata.Core.Framework;

public class CorePage : ContentPage
{
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is INavigationAwareViewModel viewModel)
            await viewModel.OnAppearingAsync();
    }

    protected override async void OnDisappearing()
    {
        base.OnDisappearing();
        if (BindingContext is INavigationAwareViewModel viewModel)
            await viewModel.OnDisappearingAsync();
    }
}
