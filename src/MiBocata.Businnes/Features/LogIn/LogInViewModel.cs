using System.Windows.Input;
using MiBocata.Businnes.Framework;
using MiBocata.Businnes.Helpers;
using Mibocata.Core.Framework;
using Models.Core;
using MiBocata.Features.LogIn;
using Mibocata.Core.Services.Interfaces;
using MiBocata.Businnes.Features.Registro;
using Mibocata.Businnes.Features.LogIn.Templates;

namespace MiBocata.Businnes.Features.LogIn;

public class LogInViewModel : CoreViewModel
{
    private Shopkeeper todoItem;
    private readonly ILogInService logInService;
    private readonly IDialogService dialogService;
    private readonly IKeyboardService keyboardService;
    private readonly IHelpYouService helpYouService;

    public LogInViewModel(
        ILogInService logInService,
        IDialogService dialogService,
        IKeyboardService keyboardService,
        IHelpYouService helpYouService)
    {
        this.logInService = logInService;
        this.dialogService = dialogService;
        this.keyboardService = keyboardService;
        this.helpYouService = helpYouService;

        CallUsCommand = new AsyncCommand(() => CallUsCommandd());
        DoLoginCommand = new AsyncCommand(() => DoLoginCommandAsync());
        GoToRegisterCommand = new AsyncCommand(() => GoToRegisterCommandAsync());
    }

    public Shopkeeper User
    {
        get => todoItem;
        set => SetAndRaisePropertyChanged(ref todoItem, value);
    }

    public ICommand CallUsCommand { get; }

    public ICommand DoLoginCommand { get; }

    public ICommand GoToRegisterCommand { get; }

    public override Task OnAppearingAsync()
    {
        User = new Shopkeeper();

#pragma warning disable CS0162 // Unreachable code detected
        if (DefaultSettings.DebugMode)
        {
            User = new Shopkeeper()
            {
                Email = "mbmdevelop@gmail.com",
                Password = "12345",
            };
        }
#pragma warning restore CS0162 // Unreachable code detected

        return base.OnAppearingAsync();
    }

    private Task CallUsCommandd() => helpYouService.CallUsAsync();

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

        await logInService.DoLoginAsync(this, User.Email, User.Password);
    }

    private async Task<bool> ValidateAsync()
    {
        if (!ValidateHelper.IsValidEmail(User.Email))
        {
            await dialogService.ShowMessage("Compruebe su email", string.Empty);
            return false;
        }

        if (!ValidateHelper.IsValidPassword(User.Password))
        {
            await dialogService.ShowMessage("La contraseña debe tener al menos 4 caracteres", string.Empty);
            return false;
        }

        return true;
    }

    private async Task GoToRegisterCommandAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(RegisterPage)}");
    }
}
