using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;


namespace TestApp.Client.Services
{
    public class AuthStateProvider(ILocalStorageService localStorage) : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await localStorage.GetItemAsStringAsync("jwt");
            if (string.IsNullOrEmpty(token))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            return new AuthenticationState(
                new ClaimsPrincipal(
                    new ClaimsIdentity("authed")
                )
            );
        }
        public async Task MarkUserAsAuthenticated(string token)
        {
            await localStorage.SetItemAsync("jwt", token);

            NotifyAuthenticationStateChanged(
                Task.FromResult(
                    new AuthenticationState(
                        new ClaimsPrincipal(
                            new ClaimsIdentity("authed")
                        )
                    )
                )
            );
        }
        public async Task MarkUserAsLoggedOut()
        {
            //empty claim
            await localStorage.RemoveItemAsync("jwt");
            NotifyAuthenticationStateChanged(
                Task.FromResult(
                    new AuthenticationState(
                        new ClaimsPrincipal(
                            new ClaimsIdentity()
                        )
                    )
                )
            );
        }
    }

}
