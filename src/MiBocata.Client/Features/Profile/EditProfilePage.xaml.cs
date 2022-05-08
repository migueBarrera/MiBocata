namespace MiBocata.Features.Profile;

public partial class EditProfilePage
{
    public EditProfilePage(EditProfileViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}