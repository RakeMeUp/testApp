using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    public class AGIQuestionCreationResponse
    {
        public string Topic { get; set; }
        public string Strictness { get; set; }
        public int MaximumTotalGrade { get; set; }
        public ICollection<AGIQuestionResponse> Questions { get; set; }
    }

    public class AGIQuestionResponse
    {
        public string QuestionText { get; set; }
        public int MaxGrade { get; set; }
    }
}
