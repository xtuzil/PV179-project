using System.ComponentModel.DataAnnotations;

namespace BL.DTOs
{
    public class UserUpdateProfileDto
    {
        [Required]
        public int Id { get; set; }
        [StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
        [Required]
        public string FirstName { get; set; }
        [StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] Avatar { get; set; }
    }
}
