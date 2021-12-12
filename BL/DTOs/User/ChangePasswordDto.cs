using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class ChangePasswordDto
    {
        [Required]
        public int Id { get; set; }
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Password confirmation does not match. Try again.")]
        public string PasswordConfirmation { get; set; }
    }
}
