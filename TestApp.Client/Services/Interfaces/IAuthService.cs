using Shared.Models;

namespace TestApp.Client.Services.Interfaces
{
    public interface IAuthService
    {
        Task Login(LoginDTO dto);
        Task Logout();
        Task Register(RegisterDTO dto);
    }
}
