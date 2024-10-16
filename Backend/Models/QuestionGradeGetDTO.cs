using Backend.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class QuestionGradeGetDTO
    {
        public long GradeId { get; set; }
        public long ResultId { get; set; }
        public long QuestionId { get; set; }
        public float GradeObtained { get; set; }
        public string Explanation { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? ApprovedAt { get; set; }
    }
}
