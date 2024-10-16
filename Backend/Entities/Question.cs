using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long QuestionId { get; set; }

        [Required]
        public long TestId { get; set; }
        public Test Test { get; set; }

        [Required]
        public string QuestionText { get; set; }

        public float MaxGrade { get; set; }

        public QuestionGrade QuestionGrade { get; set; }
    }


}
