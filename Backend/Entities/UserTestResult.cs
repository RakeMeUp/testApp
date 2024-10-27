using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities
{
    public class UserTestResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public long ResultId { get; set; }

        [Required]
        public long UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Required]
        public long TestId { get; set; }
        public Test Test { get; set; }

        public float TotalScore { get; set; }
        public float MaxScore { get; set; }

        public bool IsFinal { get; set; }

        public ICollection<QuestionGrade> QuestionGrades { get; set; }
    }


}
