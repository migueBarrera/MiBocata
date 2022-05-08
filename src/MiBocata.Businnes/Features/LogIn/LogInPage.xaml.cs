namespace MiBocata.Businnes.Features.LogIn;

public partial class LogInPage
{
    public LogInPage(LogInViewModel viewModel)
    {
        InitializeComponent();
        //NavigationPage.SetHasNavigationBar(this, false);
        BindingContext = viewModel;
    }
}