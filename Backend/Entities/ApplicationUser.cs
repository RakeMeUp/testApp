using Backend.Entities.Joins;
using Microsoft.AspNetCore.Identity;

namespace Backend.Entities
{
    public class ApplicationUser : IdentityUser<long>
    {
        public ICollection<UserCreatedTest> CreatedTests { get; set; }
        public ICollection<UserTestResult> TestResults { get; set; }
        public ICollection<UserParticipatedTest> ParticipatedTests { get; set; }
    }

}
