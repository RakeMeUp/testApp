using Backend.Entities.Joins;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities
{
    public class Test
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TestId { get; set; }

        [Required]
        [MaxLength(255)]
        public string TestTitle { get; set; }
        [Required]
        [MaxLength(255)]
        public string TestStrictness { get; set; }

        [MaxLength(1000)]
        public string TestDescription { get; set; }

        [Required]
        public long OwnerId { get; set; }
        public ApplicationUser Owner { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<UserTestResult> TestResults { get; set; }
        public ICollection<UserCreatedTest> CreatedByUsers { get; set; }
        public ICollection<UserParticipatedTest> ParticipatingUsers { get; set; }

    }



}
