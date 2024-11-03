using System.ComponentModel.DataAnnotations;

namespace Shared.Models
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
        [Required(AllowEmptyStrings =false)]
        public string AnswerText { get; set; }
    }
}
