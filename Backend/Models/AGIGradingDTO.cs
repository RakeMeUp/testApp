namespace Backend.Models
{
    public class AGIGradingDTO
    {
        public long TestId { get; set; }
        public ICollection<AGIGradingQuestionDTO> Questions { get; set; }
    }
    public class AGIGradingQuestionDTO
    {
        public long QuestionId { get; set; }
        public float Grade { get; set; }
    }
}
