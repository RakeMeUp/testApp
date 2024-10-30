using Microsoft.AspNetCore.Http;
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

        public void ApproveTest(long id, TestApproveDTO answers)
        {
            throw new NotImplementedException();
        }

        public void CreateTest(TestPostDTO post)
        {
            throw new NotImplementedException();
        }

        public void DeleteTest(long id)
        {
            throw new NotImplementedException();
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

        public void JoinTest(long id)
        {
            throw new NotImplementedException();
        }

        public void LeaveTest(long id)
        {
            throw new NotImplementedException();
        }
    }
}
