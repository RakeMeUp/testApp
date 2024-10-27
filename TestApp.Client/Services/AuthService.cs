using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.Models;
using System.Net.Http.Json;
using TestApp.Client.Services.Interfaces;

namespace TestApp.Client.Services
{
    public class AuthService(HttpClient httpClient, AuthenticationStateProvider AuthStateProvider) : IAuthService
    {
        public async Task Login(LoginDTO dto)
        {
            var resp = await httpClient.PostAsJsonAsync("auth/login", dto);
            if (resp.IsSuccessStatusCode)
            {
                var data = await resp.Content.ReadFromJsonAsync<TokenResponse>();
                var jwt = data?.Token ?? throw new Exception("Token error");

                var authProvider = (AuthStateProvider)AuthStateProvider;
                await authProvider.MarkUserAsAuthenticated(jwt);
            }
            else
            {
                throw new Exception("Login error");
            }
        }

        public async Task Logout()
        {
            var authProvider = (AuthStateProvider)AuthStateProvider;
            await authProvider.MarkUserAsLoggedOut();
        }

        public async Task Register(RegisterDTO dto)
        {
            var resp = await httpClient.PostAsJsonAsync("auth/register", dto);
            if (!resp.IsSuccessStatusCode) throw new Exception($"Register error, status code: {resp.StatusCode}");
            }
    }
}
