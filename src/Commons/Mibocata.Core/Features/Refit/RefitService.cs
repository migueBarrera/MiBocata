using System;
using System.Net.Http;
using System.Threading.Tasks;
using Mibocata.Core.Services.Interfaces;
using Refit;

namespace Mibocata.Core.Features.Refit
{
    public class RefitService : IRefitService
    {
        private IPreferencesService preferenceService;
        private IAppSecretsService appSecretsService;

        public RefitService(
            IPreferencesService preferenceService,
            IAppSecretsService appSecretsService)
        {
            this.preferenceService = preferenceService;
            this.appSecretsService = appSecretsService;
        }

        public T InitRefitInstance<T>(bool isAutenticated = false)
        {
            var handler = WireHttpHandlers();

            RefitSettings refitSettings = null;

            var httpClient = GetClient(handler, isAutenticated);

            return RestService.For<T>(httpClient, refitSettings);
        }

        private HttpClient GetClient(DelegatingHandler handler, bool isAutenticated)
        {
            HttpClient client = null;

            if (isAutenticated)
            {
                client = new HttpClient(
                           new AuthenticatedParameterizedHttpClientHandler(
                                   httpMessage =>
                                   {
                                       return Task.FromResult(GetToken());
                                   },
                                   handler));
            }
            else if (handler != null)
            {
                client = new HttpClient(handler);
            }
            else
            {
                client = new HttpClient();
            }

            client.BaseAddress = new Uri(appSecretsService.GetUrlBase());
            client.Timeout = TimeSpan.FromSeconds(5);

            return client;
        }

        private DelegatingHandler WireHttpHandlers()
        {
            DelegatingHandler handler = null;

#if DEBUG
            handler = new HttpLoggingHandler();
#endif
            ////if (DefaultSettings.ThrowExceptionsHttpRequests)
            ////{
            ////    handler = new HttpExceptionalHandler(handler);
            ////}

            return handler;
        }

        private string GetToken()
        {
            return preferenceService.GetToken();
        }
    }
}
