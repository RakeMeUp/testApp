using Backend.Entities;
using Backend.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Backend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApplicationUser?> GetCurrentUserAsync()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user != null ? await _userManager.GetUserAsync(user) : null;
        }

        public async Task<long?> GetCurrentUserIdAsync()
        {
            var user = await GetCurrentUserAsync();
            return user?.Id;
        }
    }
}
