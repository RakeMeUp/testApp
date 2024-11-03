using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    public class UserUpdateDTO
    {
        [StringLength(25)]
        [RegularExpression(@"^\S*$", ErrorMessage = "Username cannot contain spaces.")]
        public string? UserName { get; set; }
        [StringLength(100, MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).*$",
        ErrorMessage = "Password must contain at least one lowercase letter, one uppercase letter, one digit, and one special character.")]
        public string? Password { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string CurrentPassword {  get; set; }
    }
}
