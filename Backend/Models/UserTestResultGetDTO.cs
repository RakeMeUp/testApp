using Backend.Entities;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class UserTestResultGetDTO
    {
        public long ResultId { get; set; }
        public long UserId { get; set; }
        public long TestId { get; set; }
        public float TotalScore { get; set; }
        public bool IsFinal { get; set; }
    }
}
