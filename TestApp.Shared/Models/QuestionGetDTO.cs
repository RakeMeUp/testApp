namespace Shared.Models
{
    public class QuestionGetDTO
    {
        public long QuestionId { get; set; }
        public string QuestionText { get; set; }
        public float MaxGrade { get; set; }
        public ICollection<QuestionGradeGetDTO> QuestionGrades { get; set; }
    }
}
