
namespace Backend.Models
{
    public class QuestionGradeGetDTO
    {
        public long UserId { get; set; }
        public long ResultId { get; set; }
        public long QuestionId { get; set; }
        public float GradeObtained { get; set; }
        public string Explanation { get; set; }
        public string Answer { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? ApprovedAt { get; set; }
    }
}
