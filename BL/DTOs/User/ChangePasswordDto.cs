using System.ComponentModel.DataAnnotations;

namespace BL.DTOs
{
    public class ChangePasswordDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 8)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Password confirmation does not match. Try again.")]
        public string PasswordConfirmation { get; set; }
    }
}
