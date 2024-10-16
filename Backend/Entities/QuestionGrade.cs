using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities
{
    public class QuestionGrade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long GradeId { get; set; }

        [Required]
        public long ResultId { get; set; }
        public UserTestResult UserTestResult { get; set; } // Reference to the test result

        [Required]
        public long QuestionId { get; set; }
        public Question Question { get; set; } // Reference to the graded question

        public float GradeObtained { get; set; }

        public string Explanation { get; set; }

        public bool IsApproved { get; set; }

        public DateTime? ApprovedAt { get; set; }
    }


}
