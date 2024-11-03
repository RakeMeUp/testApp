using Shared.Models;

namespace TestApp.Client.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserGetDTO> GetAsync(long id);
        Task<IEnumerable<UserGetDTO>> GetByParticipationAsync(long ownerId);
        Task<HttpResponseMessage> UpdateUser(long userId, UserUpdateDTO dto);
        Task<HttpResponseMessage> DeleteUser(long userId, UserDeleteDTO deleteDto);
    }
}
