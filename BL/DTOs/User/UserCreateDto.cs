using System.ComponentModel.DataAnnotations;

namespace BL.DTOs
{
    public class UserCreateDto
    {
        [StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
        [Required]
        public string FirstName { get; set; }
        [StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Password confirmation does not match. Try again.")]
        public string PasswordConfirmation { get; set; }
    }
}
