namespace MiBocata.Businnes.Features.Registro;

public partial class RegisterPage
{
    public RegisterPage(RegisterViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}