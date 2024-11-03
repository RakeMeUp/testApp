using System.ComponentModel.DataAnnotations;

namespace Shared.Models
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
        [Range(0, int.MaxValue)]
        public float CorrectedGrade { get; set; }
        public string? Explanation { get; set; }
    }
}
