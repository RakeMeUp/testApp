
namespace Backend.Models
{
    public class AGIResponseDTO
    {
        public float FinalGrade { get; set; }
        public ICollection<AGIResponseQuestionDTO> Questions { get; set; }
    }
    public class AGIResponseQuestionDTO
    {
        public string Explanation { get; set; }
        public float Grade { get; set; }
    }
}
