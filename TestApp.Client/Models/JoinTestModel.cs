using System.ComponentModel.DataAnnotations;

namespace TestApp.Client.Models
{
    public class JoinTestModel
    {
        [Required]
        public long? TestId {  get; set; }
    }
}
