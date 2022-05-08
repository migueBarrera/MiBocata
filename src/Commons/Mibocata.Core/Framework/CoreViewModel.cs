namespace Mibocata.Core.Framework;

public class CoreViewModel : 
    ObservableObject, 
    INavigationAwareViewModel,
    IBusyViewModel
{
    private bool isBusy;

    public bool IsBusy
    {
        get => isBusy;
        set => SetAndRaisePropertyChanged(ref isBusy, value);
    }

    public virtual Task OnAppearingAsync()
    {
        return Task.CompletedTask;
    }

    public virtual Task OnDisappearingAsync()
    {
        return Task.CompletedTask;
    }
}
