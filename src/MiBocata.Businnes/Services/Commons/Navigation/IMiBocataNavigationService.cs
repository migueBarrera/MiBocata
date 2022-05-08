namespace MiBocata.Businnes.Services.Commons.Navigation
{
    public interface IMiBocataNavigationService
    {
        Task InitializeAsync();

        Task NavigateToLogIn();

        Task NavigateToRegister();

        Task NavigateToHome();

        Task NavigateToCreateStore();

        Task NavigateToChooseLocationStore();
    }
}
