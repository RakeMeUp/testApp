namespace Shared.Models
{
    public class UserTestResultGetDTO
    {
        public long ResultId { get; set; }
        public float TotalScore { get; set; }
        public float MaxScore { get; set; }
        public bool IsFinal { get; set; }
        public long UserId { get; set; }
    }
}
