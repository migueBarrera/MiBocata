using MiBocata.Features.LogIn;
using MiBocata.Framework;

namespace MiBocata.Features.Profile
{
    public partial class ProfilePage : BaseContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();

            BindingContext = Locator.Resolve<ProfileViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ((LogInControlViewModel)MyLoginControlProfile.BindingContext).InitializeAsync();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ((LogInControlViewModel)MyLoginControlProfile.BindingContext).UnitializeAsync();
        }
    }
}