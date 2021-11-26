using System.ComponentModel.DataAnnotations;

namespace BL.DTOs
{
    public class UserCreateDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Password confirmation does not match. Try again.")]
        public string PasswordConfirmation { get; set; }
    }
}
