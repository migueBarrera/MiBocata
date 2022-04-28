using Mibocata.Core.Features.Auth;
using Mibocata.Core.Features.Refit;
using Mibocata.Core.Services.Interfaces;
using MiBocata.Services.NavigationService;
using Models.Core;
using Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiBocata.Features.Register
{
    internal class RegisterService : IRegisterService
    {
        private readonly IAuthApi authApi;
        private readonly ILoggingService loggingService;
        private readonly ITaskHelperFactory taskHelperFactory;
        private readonly IMiBocataNavigationService navigationService;
        private readonly IPreferencesService preferencesService;

        public RegisterService(
            IRefitService refitService,
            ILoggingService loggingService,
            ITaskHelperFactory taskHelperFactory, 
            IMiBocataNavigationService navigationService, 
            IPreferencesService preferencesService)
        {
            this.authApi = refitService.InitRefitInstance<IAuthApi>();
            this.loggingService = loggingService;
            this.taskHelperFactory = taskHelperFactory;
            this.navigationService = navigationService;
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

        private async Task RegisterSuccessesful(Client result)
        {
            preferencesService.SetClient(result);
            await navigationService.NavigateToHome();
        }
    }
}
