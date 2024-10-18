using Backend.Entities;

namespace Backend.Models
{
    public class TestGetDTO
    {
        public long TestId { get; set; }
        public string TestTitle { get; set; }
        public string TestDescription { get; set; }
        public long OwnerId { get; set; }
        public ICollection<QuestionGetDTO> Questions { get; set; }
        public ICollection<UserTestResultGetDTO> TestResults { get; set; }
        public ICollection<long> ParticipatedUserIDs { get; set; }
    }
}
