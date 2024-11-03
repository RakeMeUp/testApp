using OpenAI.Chat;
using Shared.Models;
using System.Text;

namespace Backend.Config
{
    public class AGIQuestionCreationConfig
    {
        public static readonly string PrePrompt = """
        You are a teacher assistant supporting the teacher creating a test.
        The teacher is asking you to create questions for his test.
        You will get a Topic which you will make the questions about,
        a Strictness which determines how hard and deep the questions should be,
        also sets the expectations for the evaluating teacher how hard they should grade the test.
        Questions should have a question text, and a maximum achiavable grade for that one question.
        You will recieve a Maximum Total Grade, please make sure the max grade for the questions add up to that number.
        Grades should be whole numbers.
        Please do not number the questions.
        You will recieve NumberOfQuestions, please make sure you give as many questions as many is specified in it.
        """;
        public static ChatResponseFormat CreateResponseFormat()
        {
            byte[] schema = """
            {
                "type": "object",
                "properties": {
                    "topic":{"type": "string"},
                    "strictness":{"type": "string"},
                    "maximum_total_grade":{"type": "number"},
                    "questions": {
                        "type": "array",
                        "items": {
                            "type": "object",
                            "properties": {
                                "question_text": {"type": "string"},
                                "max_grade": { "type": "number"}
                            },
                            "required": ["question_text","max_grade"],
                            "additionalProperties": false
                        }
                    }
                },
                "required": ["topic","strictness","maximum_total_grade","questions"],
                "additionalProperties": false
            }
            """u8.ToArray();

            return ChatResponseFormat.CreateJsonSchemaFormat(
                jsonSchemaFormatName: "question_creation",
                jsonSchema: BinaryData.FromBytes(schema),
                jsonSchemaIsStrict: true
            );
        }

        public static string CreationDTOToPrompt(AGIQuestionCreationDTO dto)
        {
            StringBuilder sb = new();
            sb.Append($"{PrePrompt}:\n")
                .Append($"Topic:{dto.Topic}\n")
                .Append($"Strictness:{dto.Strictness}\n")
                .Append($"MaximumTotalGrade:{dto.MaximumTotalGrade}\n")
                .Append($"NumberOfQuestions:{dto.NumberOfQuestions}\n");
            return sb.ToString();
        }
    }
}
