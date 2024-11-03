using System.ComponentModel.DataAnnotations;

namespace TestApp.Client.Models
{
    public class IntermediateApproveDTO
    {
        [Required]
        public long UserId { get; set; }
        public IEnumerable<IntermediateApproveGradeDTO> GradeEdits { get; set; }
    }
    public class IntermediateApproveGradeDTO
    {
        [Required]
        public long QuestionId { get; set; }
        [Range(0, int.MaxValue)]
        public float? CorrectedGrade { get; set; }
        public string? Explanation { get; set; }
    }
}
