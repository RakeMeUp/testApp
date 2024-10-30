using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace TestApp.Client.Services
{
    public class AuthHandler(ILocalStorageService localStorage) : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var jwt = await localStorage.GetItemAsync<string>("jwt", cancellationToken);
            if (!string.IsNullOrEmpty(jwt))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }

}
