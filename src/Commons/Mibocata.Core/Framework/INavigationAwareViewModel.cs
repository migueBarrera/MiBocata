namespace Mibocata.Core.Framework;

public interface INavigationAwareViewModel
{
    Task OnAppearingAsync();
    Task OnDisappearingAsync();
}
