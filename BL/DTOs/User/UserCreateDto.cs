using System.ComponentModel.DataAnnotations;

namespace BL.DTOs
{
    public class UserCreateDto
    {
        [StringLength(100)]
        [Required]
        public string FirstName { get; set; }
        [StringLength(100)]
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 8)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Password confirmation does not match. Try again.")]
        public string PasswordConfirmation { get; set; }
    }
}
