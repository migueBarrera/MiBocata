namespace MiBocata.Features.LogIn;

public class LogInViewModel : CoreViewModel
{
    private readonly LogInControlViewModel _control;
    public LogInViewModel(LogInControlViewModel viewModel)
    {
        _control = viewModel;
    }

    public LogInControlViewModel Control => _control;

    public override async Task OnAppearingAsync()
    {
        await base.OnAppearingAsync();
        await Control.OnAppearingAsync();
    }
}
