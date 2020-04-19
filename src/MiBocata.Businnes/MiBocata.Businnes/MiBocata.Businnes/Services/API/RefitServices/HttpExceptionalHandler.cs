using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiBocata.Businnes.Services.API.RefitServices
{
    public class HttpExceptionalHandler : DelegatingHandler
    {
        private const int PercentageOfNetworkErrors = 10;

        private static readonly Random random = new Random();

        public HttpExceptionalHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            var throwException = random.Next(100) < PercentageOfNetworkErrors;
            if (throwException)
            {
                throw new HttpRequestException();
            }

            return response;
        }
    }
}
