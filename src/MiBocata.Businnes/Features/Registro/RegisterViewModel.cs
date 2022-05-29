using System.Windows.Input;
using MiBocata.Businnes.Helpers;
using Mibocata.Core.Framework;
using Mibocata.Core.Services.Interfaces;
using Models.Core;
using Mibocata.Businnes.Features.LogIn.Templates;

namespace MiBocata.Businnes.Features.Registro;

public class RegisterViewModel : CoreViewModel
{
    private Shopkeeper todoItem;
    private readonly IRegisterService registerService;
    private readonly IDialogService dialogService;
    private readonly IKeyboardService keyboardService;
    private readonly IHelpYouService helpYouService;

    public RegisterViewModel(
        IRegisterService registerService,
        IDialogService dialogService,
        IKeyboardService keyboardService,
        IHelpYouService helpYouService)
    {
        this.registerService = registerService;
        this.dialogService = dialogService;
        this.keyboardService = keyboardService;
        this.helpYouService = helpYouService;
    }

    public Shopkeeper User
    {
        get => todoItem;
        set => SetAndRaisePropertyChanged(ref todoItem, value);
    }

    public ICommand RegisterCommand => new AsyncCommand(() => RegisterCommandAsync());

    public ICommand CallUsCommand => new AsyncCommand(() => CallUsCommandd());

    public override async Task OnAppearingAsync()
    {
        User = new Shopkeeper();

        await base.OnAppearingAsync();
    }

    private Task CallUsCommandd() => helpYouService.CallUsAsync();

    private async Task RegisterCommandAsync()
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

        await registerService.RegisterCommandAsync(User);
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
}
