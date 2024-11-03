using Shared.Models;
using System.Net.Http;
using System.Net.Http.Json;
using TestApp.Client.Services.Interfaces;

namespace TestApp.Client.Services
{
    public class UserService(HttpClient httpClient) : IUserService
    {
        public async Task<UserGetDTO> GetAsync(long id)
        {
            return await httpClient.GetFromJsonAsync<UserGetDTO>($"users/{id}");
        }

        public async Task<IEnumerable<UserGetDTO>> GetByParticipationAsync(long testId)
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<UserGetDTO>>($"users/test/{testId}");

        }
        public async Task<HttpResponseMessage> UpdateUser(long userId, UserUpdateDTO dto)
        {
            return await httpClient.PutAsJsonAsync($"users/{userId}", dto);
        }
        public async Task<HttpResponseMessage> DeleteUser(long userId, UserDeleteDTO dto)
        {
            return await httpClient.PostAsJsonAsync($"users/{userId}/delete",dto);
        }
    }
}
