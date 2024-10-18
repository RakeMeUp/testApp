using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class TestEvaluationDTO
    {
        [Required]
        public long TestId { get; set; }
        [Required]
        [MaxLength(255)]
        public string TestDescription { get; set; }
        [Required]
        public ICollection<QuestionsAndAnswer> QuestionsAndAnswers { get; set; }
    }

    public class QuestionsAndAnswer
    {
        [Required]
        public long QuestionId { get; set; }
        [Required]
        public string QuestionText { get; set; }
        [Required]
        public string AnswerText { get; set; }
        [Required]
        public float MaxGrade { get; set; }

    }
}
