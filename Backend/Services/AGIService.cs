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
            var prompt = AGIEvaluationConfig.EvalDTOToPrompt(dto) ?? throw new ArgumentNullException(nameof(dto));
            List<ChatMessage> messages =
                [
                    new UserChatMessage(prompt),
                ];

            ChatCompletionOptions options = new()
            {
                ResponseFormat = AGIEvaluationConfig.CreateResponseFormat()
            };

            ChatCompletion completion = await client.CompleteChatAsync(messages, options);

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

        public async Task<AGIQuestionCreationResponse> CreateQuestion(AGIQuestionCreationDTO dto)
        {
            var prompt = AGIQuestionCreationConfig.CreationDTOToPrompt(dto) ?? throw new ArgumentNullException(nameof(dto));
            List<ChatMessage> messages =
                [
                    new UserChatMessage(prompt),
                ];

            ChatCompletionOptions options = new()
            {
                ResponseFormat = AGIQuestionCreationConfig.CreateResponseFormat()
            };

            ChatCompletion completion = await client.CompleteChatAsync(messages, options);

            using JsonDocument structuredJson = JsonDocument.Parse(completion.Content[0].Text);

            var resp = new AGIQuestionCreationResponse()
            {
                Topic = structuredJson.RootElement.GetProperty("topic").ToString(),
                Strictness = structuredJson.RootElement.GetProperty("strictness").ToString(),
                MaximumTotalGrade = structuredJson.RootElement.GetProperty("maximum_total_grade").GetInt32(),
                Questions = []
            };
            foreach (var q in structuredJson.RootElement.GetProperty("questions").EnumerateArray())
            {
                resp.Questions.Add(new AGIQuestionResponse()
                {
                    QuestionText = q.GetProperty("question_text").GetString(),
                    MaxGrade = q.GetProperty("max_grade").GetInt32()
                }
                );
            }
            return resp;
        }
    }
}
