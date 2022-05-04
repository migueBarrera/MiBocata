namespace MiBocata.Features.LogIn;

public partial class LogInPage : BaseContentPage
{
    public LogInPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        ((LogInControlViewModel)MyLoginControl.BindingContext).InitializeAsync();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        ((LogInControlViewModel)MyLoginControl.BindingContext).UnitializeAsync();
    }
}