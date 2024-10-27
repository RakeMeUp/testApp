using Backend.Config;
using Backend.Models;
using Backend.Services.Interfaces;
using OpenAI.Chat;
using Shared.Models;
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

        public async Task<AGIEvaluateResponseDTO> Evaluate(TestEvaluationDTO dto)
        {
            var prompt = AGIEvaluationConfig.DTOToPrompt(dto) ?? throw new ArgumentNullException(nameof(dto));
            List<ChatMessage> messages =
                [
                    new UserChatMessage(prompt),
                ];

            ChatCompletionOptions options = new()
            {
                ResponseFormat = AGIEvaluationConfig.CreateResponseFormat()
            };

            ChatCompletion completion = await client.CompleteChatAsync(messages, options);
            Console.WriteLine(completion.Content[0].Text);

            using JsonDocument structuredJson = JsonDocument.Parse(completion.Content[0].Text);

            var resp = new AGIEvaluateResponseDTO()
            {
                TestId = structuredJson.RootElement.GetProperty("test_id").GetInt64(),
                Questions = []
            };
            foreach (var q in structuredJson.RootElement.GetProperty("questions").EnumerateArray())
            {
                resp.Questions.Add(new AGIEvaluateResponseQuestionDTO()
                    {
                        QuestionId = q.GetProperty("question_id").GetInt64(),
                        Explanation = q.GetProperty("explanation").ToString(),
                        Grade = q.GetProperty("grade").GetSingle(),
                        MaxGrade = q.GetProperty("max_grade").GetSingle()
                }
                );
            }
            return resp;
        }
    }
}
