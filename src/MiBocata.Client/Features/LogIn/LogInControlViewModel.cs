using System.Windows.Input;
using Mibocata.Core.Features.Auth;
using Mibocata.Core.Features.Refit;
using MiBocata.Features.Register;

namespace MiBocata.Features.LogIn;

public class LogInControlViewModel : CoreViewModel
{
    private readonly IAuthApi authApi;
    private readonly IPreferencesService preferencesService;
    private readonly ILoggingService loggingService;
    private readonly IDialogService dialogService;
    private readonly ITaskHelperFactory taskHelperFactory;
    private readonly IKeyboardService keyboardService;
    private Models.Core.Client client;

    public LogInControlViewModel(
        IPreferencesService preferencesService,
        ILoggingService loggingService,
        IDialogService dialogService,
        IRefitService refitService,
        ITaskHelperFactory taskHelperFactory,
        IKeyboardService keyboardService)
    {
        this.authApi = refitService.InitRefitInstance<IAuthApi>();
        this.preferencesService = preferencesService;
        this.loggingService = loggingService;
        this.dialogService = dialogService;
        this.taskHelperFactory = taskHelperFactory;
        this.keyboardService = keyboardService;
    }

    public Models.Core.Client User
    {
        get => client;
        set => SetAndRaisePropertyChanged(ref client, value);
    }

    public ICommand DoLoginCommand => new AsyncCommand(() => DoLoginCommandAsync());

    public ICommand GoToRegisterCommand => new AsyncCommand(() => GoToRegisterCommandAsync());

    public override Task OnAppearingAsync()
    {
        User = new Models.Core.Client();

#pragma warning disable CS0162 // Unreachable code detected
        if (DefaultSettings.DebugMode)
        {
            User = new Models.Core.Client()
            {
                Email = "shopkeeper@email.com",
                Password = "123456",
            };
        }
#pragma warning restore CS0162 // Unreachable code detected

        return base.OnAppearingAsync();
    }

    private async Task DoLoginCommandAsync()
    {
        if (IsBusy)
        {
            return;
        }

        keyboardService.HideSoftKeyboard();

        if (!await ValidateAsync())
        {
            return;
        }

        var result = await taskHelperFactory.
                                CreateInternetAccessViewModelInstance(loggingService, this).
                                TryExecuteAsync(
                                () => authApi.SignIn(new Models.Requests.ClientSignInRequest()
                                {
                                    Email = User.Email, 
                                    Password = User.Password,
                                }));

        if (result)
        {
            await LogInSuccessful(ClientSignInResponse.Parse(result.Value));
        }
    }

    private async Task<bool> ValidateAsync()
    {
        if (!ValidateHelper.IsValidEmail(User.Email))
        {
            await dialogService.ShowAlertAsync("Compruebe su email", string.Empty);
            return false;
        }

        if (!ValidateHelper.IsValidPassword(User.Password))
        {
            await dialogService.ShowAlertAsync("La contraseña debe tener al menos 4 caracteres", string.Empty);
            return false;
        }

        return true;
    }

    private Task LogInSuccessful(Models.Core.Client client)
    {
        preferencesService.SetClient(client);
        //await navigationService.NavigateToHome();
        App.Current.MainPage = new AppHomeShell();

        return Task.CompletedTask;
    }

    private async Task GoToRegisterCommandAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(RegisterPage)}");
    }
}
