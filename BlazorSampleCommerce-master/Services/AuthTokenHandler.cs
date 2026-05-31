using System.Net.Http.Headers;

namespace BlazorSampleCommerce.Services
{
    public class AuthTokenHandler : DelegatingHandler
    {
        private readonly TokenStore _tokenStore;

        public AuthTokenHandler(TokenStore tokenStore)
        {
            _tokenStore = tokenStore;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(_tokenStore.Token))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _tokenStore.Token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
