using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class TestAnswerDTO
    {
        [Required]
        public ICollection<Answer> Answers { get; set; }
    }

    public class Answer
    {
        [Required]
        public long QuestionId { get; set; }
        [Required]
        public string AnswerText { get; set; }
    }
}
