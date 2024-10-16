using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class TestPostDTO
    {
        [Required]
        [MaxLength(255)]
        public string TestTitle { get; set; }
        [MaxLength(1000)]
        public string TestDescription { get; set; }
        public ICollection<QuestionPostDTO> Questions { get; set; }

    }
}
