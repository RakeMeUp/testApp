namespace Backend.Entities
{
    public class QuestionGrade
    {
        public long UserId { get; set; }
        public long ResultId { get; set; }
        public long QuestionId { get; set; }

        public UserTestResult UserTestResult { get; set; }
        public Question Question { get; set; }

        public float GradeObtained { get; set; }
        public float MaxGrade { get; set; }
        public string Explanation { get; set; }
        public string Answer { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? ApprovedAt { get; set; }
    }
}
