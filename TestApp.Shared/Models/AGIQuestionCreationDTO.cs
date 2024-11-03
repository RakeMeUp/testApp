using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    public class AGIQuestionCreationDTO
    {
        [Required]
        [MaxLength(5000)]
        public string Topic { get; set; }
        [Required]
        [MaxLength(255)]
        public string Strictness { get; set; }
        [Range(1, 255)]
        public int MaximumTotalGrade { get; set; } = 100;
        [Range(1, 255)]
        public int NumberOfQuestions { get; set; } = 1;
    }
}
