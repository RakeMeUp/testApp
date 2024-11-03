using Shared.Models;
using System.Net.Http;
using System.Net.Http.Json;
using TestApp.Client.Pages;
using TestApp.Client.Services.Interfaces;

namespace TestApp.Client.Services
{
    public class QuestionService(HttpClient httpClient) : IQuestionService
    {
        public async Task<AGIQuestionCreationResponse> CreateQuestions(AGIQuestionCreationDTO dto)
        {

            var resp = await httpClient.PostAsJsonAsync("questions", dto);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<AGIQuestionCreationResponse>();
        }
    }
}
