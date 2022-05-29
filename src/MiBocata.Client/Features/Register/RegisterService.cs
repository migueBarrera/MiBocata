using Mibocata.Core.Features.Auth;
using Mibocata.Core.Features.Refit;

namespace MiBocata.Features.Register;

internal class RegisterService : IRegisterService
{
    private readonly IAuthApi authApi;
    private readonly ILoggingService loggingService;
    private readonly ITaskHelperFactory taskHelperFactory;
    private readonly IPreferencesService preferencesService;

    public RegisterService(
        IRefitService refitService,
        ILoggingService loggingService,
        ITaskHelperFactory taskHelperFactory, 
        IPreferencesService preferencesService)
    {
        this.authApi = refitService.InitRefitInstance<IAuthApi>();
        this.loggingService = loggingService;
        this.taskHelperFactory = taskHelperFactory;
        this.preferencesService = preferencesService;
    }

    public async Task DoRegisterAsync(string email, string password, string name)
    {
        var result = await taskHelperFactory.
                           CreateInternetAccessViewModelInstance(loggingService/*, this*/).
                           TryExecuteAsync(
                           () => authApi.SignUp(new Models.Requests.ClientSignUpRequest()
                           {
                               Email = email,
                               Name = name,
                               Password = password,
                           }));

        if (result)
        {
            await RegisterSuccessesful(ClientSignUpResponse.Parse(result.Value));
        }
    }

    private Task RegisterSuccessesful(Models.Core.Client result)
    {
        preferencesService.SetClient(result);
        App.Current.MainPage = new AppHomeShell();
        return Task.CompletedTask;
    }
}
