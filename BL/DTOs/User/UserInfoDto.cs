
using CactusDAL.Models;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs
{
    public class UserInfoDto
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
        [Required]
        public string Password { get; set; }
        public bool Banned { get; set; }
        public double AccountBalance { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public Role Role { get; set; }
    }
}
