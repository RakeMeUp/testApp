using Backend.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class QuestionGetDTO
    {
        public long QuestionId { get; set; }
        public string QuestionText { get; set; }
        public float MaxGrade { get; set; }
        public QuestionGradeGetDTO QuestionGrade { get; set; }
    }
}
