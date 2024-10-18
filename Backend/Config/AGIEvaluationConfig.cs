using Backend.Models;
using Microsoft.JSInterop.Infrastructure;
using OpenAI.Chat;
using System.Text;

namespace Backend.Config
{
    public static class AGIEvaluationConfig
    {
        public static readonly string PrePrompt = "A student took a quiz with the given topic, here are the questions and the answers the student gave for them. Please evaluate each question and grade them with the proper explanation for each. Grading must be between 0 and 1 for each question with the increments of 0.25";
        public static ChatResponseFormat CreateResponseFormat()
        {
            byte[] schema = """
            {
                "type": "object",
                "properties": {
                    "test_id":{"type": "number"},
                    "questions": {
                        "type": "array",
                        "items": {
                            "type": "object",
                            "properties": {
                                "question_id": {"type": "number"},
                                "explanation": { "type": "string" },
                                "grade": { "type": "number"}
                            },
                            "required": ["question_id","explanation", "grade"],
                            "additionalProperties": false
                        }
                    }
                },
                "required": ["test_id","questions"],
                "additionalProperties": false
            }
            """u8.ToArray();

            return ChatResponseFormat.CreateJsonSchemaFormat(
                jsonSchemaFormatName: "test_eval",
                jsonSchema: BinaryData.FromBytes(schema),
                jsonSchemaIsStrict: true
            );
        }

        public static string DTOToPrompt(TestEvaluationDTO dto)
        {
            StringBuilder sb = new();
            sb  .Append($"{PrePrompt}:\n")
                .Append($"TestId:{dto.TestId}\n")
                .Append($"TestDescription:{dto.TestDescription}\n");
                
            foreach (var q in dto.QuestionsAndAnswers)
            {
                sb  .Append($"Question(id:{q.QuestionId}): {q.QuestionText}\n")
                    .Append($"Answer(id:{q.QuestionId}): {q.AnswerText}\n");
            }
            return sb.ToString();
        }

    }
}
