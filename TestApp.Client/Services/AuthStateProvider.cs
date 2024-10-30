using System.IdentityModel.Tokens.Jwt;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;


namespace TestApp.Client.Services
{
    public class AuthStateProvider(ILocalStorageService localStorage) : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await localStorage.GetItemAsync<string>("jwt");
            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine("token null");
                return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
            }
            var claims = GetClaims(token);
            var authState = new AuthenticationState(
                                new ClaimsPrincipal(
                                    new ClaimsIdentity(claims, "jwt")
                                )
                            );
            return await Task.FromResult(authState);
        }
        public async Task MarkUserAsAuthenticated(string token)
        {
            await localStorage.SetItemAsync("jwt", token);

            var claims = GetClaims(token);
            var authState = new AuthenticationState(
                                new ClaimsPrincipal(
                                    new ClaimsIdentity(claims, "jwt")
                                )
                            );
            NotifyAuthenticationStateChanged(
                Task.FromResult(authState)
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
        public async Task<long> GetCurrentUserId()
        {
            var token = await localStorage.GetItemAsync<string>("jwt");

            var handler = new JwtSecurityTokenHandler();
            var t = handler.ReadJwtToken(token);
            var id = t.Claims.First(c => c.Type == "sub").Value ?? throw new Exception("No SUB claim found in token");
            return long.Parse(id);
        }
        private List<Claim> GetClaims(string token)
        {
            List<Claim> claims = [];
            var handler = new JwtSecurityTokenHandler();
            var t = handler.ReadJwtToken(token);
            foreach(var claim in t.Claims)
            {
                claims.Add(claim);
            }
            claims.Add(
                new Claim(
                    ClaimTypes.Name, t.Claims.First(c => c.Type == "unique_name").Value
                )
            );
            return claims;
        }
    }

}
