﻿using System.Net.Http.Headers;

namespace Mibocata.Core.Features.Refit
{
    public class AuthenticatedParameterizedHttpClientHandler : DelegatingHandler
    {
        private readonly Func<HttpRequestMessage, Task<string>> getToken;

        public AuthenticatedParameterizedHttpClientHandler(Func<HttpRequestMessage, Task<string>> getToken, HttpMessageHandler innerHandler = null)
            : base(innerHandler ?? new HttpClientHandler())
        {
            this.getToken = getToken ?? throw new ArgumentNullException(nameof(getToken));
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // See if the request has an authorize header
            var auth = request.Headers.Authorization;
            if (auth != null)
            {
                var token = await getToken(request).ConfigureAwait(false);
                request.Headers.Authorization = new AuthenticationHeaderValue(auth.Scheme, token);
            }

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
