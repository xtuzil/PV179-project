
using CactusDAL.Models;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs
{
    public class UserInfoDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Banned { get; set; }
        public double AccountBalance { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] Avatar { get; set; }
        public Role Role { get; set; }
    }
}
