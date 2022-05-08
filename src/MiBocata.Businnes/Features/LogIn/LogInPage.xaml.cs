namespace MiBocata.Businnes.Features.LogIn;

public partial class LogInPage
{
    public LogInPage(LogInViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}