using Backend.Entities;

namespace Backend.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser?> GetCurrentUserAsync();
        Task<long?> GetCurrentUserIdAsync();
    }
}
