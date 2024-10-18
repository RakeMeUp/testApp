
namespace Backend.Models
{
    public class AGIEvaluateResponseDTO
    {
        public long TestId { get; set; }
        public ICollection<AGIEvaluateResponseQuestionDTO> Questions { get; set; }
    }
    public class AGIEvaluateResponseQuestionDTO
    {
        public long QuestionId { get; set; }
        public string Explanation { get; set; }
        public float Grade { get; set; }
    }
}
