namespace MiBocata.Features.Register;

public partial class RegisterPage
{
    public RegisterPage(RegisterViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}