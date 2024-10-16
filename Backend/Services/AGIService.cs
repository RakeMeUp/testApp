using Backend.Config;
using Backend.Models;
using Backend.Services.Interfaces;
using OpenAI.Chat;
using System.Text.Json;

namespace Backend.Services
{
    public class AGIService: IAGIService
    {
        private readonly string? _AGIApiKey;
        private readonly ChatClient client;

        public AGIService(IConfiguration configuration)
        {
            _AGIApiKey = configuration["OpenAI:ApiKey"];
            client = new ChatClient(model: "gpt-4o", apiKey: _AGIApiKey);
        }

        public async Task<AGIResponseDTO> Grade(string prompt)
        {
            List<ChatMessage> messages =
                [
                    new UserChatMessage(prompt),
                ];

            ChatCompletionOptions options = new()
            {
                ResponseFormat = TestEvaluationResponseFormat.CreateResponseFormat()
            };

            ChatCompletion completion = await client.CompleteChatAsync(messages, options);

            using JsonDocument structuredJson = JsonDocument.Parse(completion.Content[0].Text);

            var dto = new AGIResponseDTO()
            {
                FinalGrade = structuredJson.RootElement.GetProperty("final_grade").GetSingle(),
                Questions = new List<AGIResponseQuestionDTO>()
            };
            foreach (var q in structuredJson.RootElement.GetProperty("questions").EnumerateArray())
            {
                dto.Questions.Add(new AGIResponseQuestionDTO()
                    {
                        Explanation = q.GetProperty("explanation").ToString(),
                        Grade = q.GetProperty("grade").GetSingle()
                    }
                );
            }
            return dto;
        }

    }
}
