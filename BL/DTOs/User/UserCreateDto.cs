using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.User
{
    public class UserCreateDto
    { 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Banned { get; set; }
        public double AccountBalance { get; set; }
        public string PhoneNumber { get; set; }

        public int AddressId { get; set; }


    }
}
