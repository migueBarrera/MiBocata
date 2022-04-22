using System;
using System.Net.Http;
using System.Threading.Tasks;
using Mibocata.Core.Features.Refit;
using MiBocata.Framework;
using MiBocata.Services.PreferencesService;

namespace MiBocata.Services.API.RefitServices
{
    public class RefitService : IRefitService
    {
        private IPreferencesService preferenceService;

        public RefitService(IPreferencesService preferenceService)
        {
            this.preferenceService = preferenceService;
        }

        public T InitRefitInstance<T>(bool isAutenticated = false)
        {
            var handler = WireHttpHandlers();

            Refit.RefitSettings refitSettings = null;

            var httpClient = GetClient(handler, isAutenticated);

            return Refit.RestService.For<T>(httpClient, refitSettings);
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

            client.BaseAddress = new Uri(DefaultSettings.URL_BASE);
            client.Timeout = TimeSpan.FromSeconds(5);

            return client;
        }

        private DelegatingHandler WireHttpHandlers()
        {
            DelegatingHandler handler = null;

#pragma warning disable CS0162 // Se detectó código inaccesible
            if (DefaultSettings.DebugMode)
            {
                handler = new HttpLoggingHandler();
            }

            ////if (DefaultSettings.ThrowExceptionsHttpRequests)
            ////{
            ////    handler = new HttpExceptionalHandler(handler);
            ////}
#pragma warning restore CS0162 // Se detectó código inaccesible

            return handler;
        }

        private string GetToken()
        {
            return preferenceService.GetToken();
        }
    }
}
