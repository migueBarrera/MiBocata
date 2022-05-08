using System.Windows.Input;

namespace MiBocata.Features.Register;

public class RegisterViewModel : CoreViewModel
{
    private Models.Core.Client user;

    private readonly IDialogService dialogService;
    private readonly IRegisterService registerService;
    private readonly IKeyboardService keyboardService;

    public RegisterViewModel(
        IDialogService dialogService,
        IRegisterService registerService,
        IKeyboardService keyboardService)
    {
        this.dialogService = dialogService;
        this.registerService = registerService;
        this.keyboardService = keyboardService;
    }

    public Models.Core.Client User
    {
        get => user;
        set => SetAndRaisePropertyChanged(ref user, value);
    }

    public ICommand RegisterCommand => new AsyncCommand(() => RegisterCommandAsync());

    public override async Task OnAppearingAsync()
    {
        User = new Models.Core.Client();

        await base.OnAppearingAsync();
    }

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

        await registerService.DoRegisterAsync(user.Email, user.Password, user.Name);
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
