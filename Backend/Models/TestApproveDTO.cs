using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class TestApproveDTO
    {
        [Required]
        public long UserId { get; set; }
        public IEnumerable<TestApproveGradeDTO> GradeEdits { get; set; }
    }
    public class TestApproveGradeDTO
    {
        [Required]
        public long QuestionId { get; set; }
        [Required]
        public float CorrectedGrade { get; set; }
        public string? Explanation { get; set; }
    }
}
