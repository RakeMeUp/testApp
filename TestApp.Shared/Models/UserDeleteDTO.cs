using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    public class UserDeleteDTO
    {
        [Required]
        public string Password { get; set; }
    }
}
