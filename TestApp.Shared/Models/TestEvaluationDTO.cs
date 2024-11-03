using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    public class TestEvaluationDTO
    {
        [Required]
        public long TestId { get; set; }
        [Required]
        [MaxLength(255)]
        public string TestDescription { get; set; }
        [MaxLength(255)]
        public string TestStrictness { get; set; }
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
