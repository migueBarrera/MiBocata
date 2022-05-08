namespace MiBocata.Features.Profile;

public partial class ProfilePage
{
    public ProfilePage(ProfileViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}