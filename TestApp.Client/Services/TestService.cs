using Shared.Models;
using System.Net.Http.Json;
using TestApp.Client.Services.Interfaces;

namespace TestApp.Client.Services
{
    public class TestService(HttpClient httpClient) : ITestService
    {
        public async Task<HttpResponseMessage> AnswerTest(long testId, TestAnswerDTO answers)
        {
            return await httpClient.PostAsJsonAsync($"tests/{testId}/answer", answers);
        }

        public async Task<HttpResponseMessage> ApproveTest(long testId, TestApproveDTO approvalDTO)
        {
            return await httpClient.PostAsJsonAsync($"tests/{testId}/approve", approvalDTO);
        }

        public async Task<HttpResponseMessage> CreateTest(TestPostDTO post)
        {
            return await httpClient.PostAsJsonAsync("tests", post);
        }

        public async Task<HttpResponseMessage> DeleteTest(long id)
        {
            return await httpClient.DeleteAsync($"tests/{id}");
        }

        public async Task<TestGetDTO> GetAsync(long id)
        {
            return await httpClient.GetFromJsonAsync<TestGetDTO>($"tests/{id}");
        }

        public async Task<IEnumerable<TestGetDTO>> GetByOwnerAsync(long ownerId)
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<TestGetDTO>>($"tests/owner/{ownerId}");
        }

        public async Task<IEnumerable<TestGetDTO>> GetByParticipationAsync(long ownerId)
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<TestGetDTO>>($"tests/participation/{ownerId}");
        }

        public async Task<HttpResponseMessage> JoinTest(long id)
        {
            return await httpClient.PostAsync($"tests/{id}/join", null);
        }

        public async Task<HttpResponseMessage> LeaveTest(long id)
        {
            return await httpClient.PostAsync($"tests/{id}/leave",null);
        }
    }
}
