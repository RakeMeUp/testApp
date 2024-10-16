using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class QuestionPostDTO
    {
        [Required]
        public string QuestionText { get; set; }
        public float MaxGrade { get; set; }
    }
}
