namespace MiBocata.Businnes.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new MiBocata.Businnes.App(new PlatformDependencies()));
        }
    }
}
