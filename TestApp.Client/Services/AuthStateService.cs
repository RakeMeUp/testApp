using Shared.Models;
using TestApp.Client.Services.Interfaces;

namespace TestApp.Client.Services
{
    public class AuthStateService(IAuthService authService)
    {
        private bool isAuthenticated = false;
        public bool IsAuthenticated
        {
            get => isAuthenticated;
            private set
            {
                if (isAuthenticated != value)
                {
                    isAuthenticated = value;
                    OnAuthenticationStateChanged?.Invoke(isAuthenticated);
                }
            }
        }

        public event Action<bool> OnAuthenticationStateChanged;

        public async Task Login(LoginDTO dto)
        {
            await authService.Login(dto);
            IsAuthenticated = true;
        }

        public async Task Logout()
        {
            await authService.Logout();
            IsAuthenticated = false;
        }
    }

}
