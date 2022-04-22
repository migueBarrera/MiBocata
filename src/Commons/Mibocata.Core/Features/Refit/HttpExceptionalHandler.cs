using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Mibocata.Core.Features.Refit
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
