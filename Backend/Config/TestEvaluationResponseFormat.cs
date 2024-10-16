using OpenAI.Chat;

namespace Backend.Config
{
    public static class TestEvaluationResponseFormat
    {
        public static ChatResponseFormat CreateResponseFormat()
        {
            byte[] schema = """
            {
                "type": "object",
                "properties": {
                    "questions": {
                        "type": "array",
                        "items": {
                            "type": "object",
                            "properties": {
                                "explanation": { "type": "string" },
                                "grade": { "type": "number"}
                            },
                            "required": ["explanation", "grade"],
                            "additionalProperties": false
                        }
                    },
                    "final_grade": { "type": "number"}
                },
                "required": ["questions", "final_grade"],
                "additionalProperties": false
            }
            """u8.ToArray();

            return ChatResponseFormat.CreateJsonSchemaFormat(
                jsonSchemaFormatName: "test_eval",
                jsonSchema: BinaryData.FromBytes(schema),
                jsonSchemaIsStrict: true
            );
        }
    }
}
