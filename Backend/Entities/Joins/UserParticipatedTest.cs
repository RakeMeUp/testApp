using System.ComponentModel.DataAnnotations;

namespace Backend.Entities.Joins
{
    public class UserParticipatedTest
    {
        [Key]
        public long UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Key]
        public long TestId { get; set; }
        public Test Test { get; set; }
    }

}
