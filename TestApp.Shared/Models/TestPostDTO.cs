using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    public class TestPostDTO
    {
        [Required]
        [MaxLength(255)]
        public string TestTitle { get; set; }
        [Required]
        [MaxLength(255)]
        public string TestStrictness { get; set; }
        [MaxLength(1000)]
        public string TestDescription { get; set; }
        public ICollection<QuestionPostDTO> Questions { get; set; }

    }
}
