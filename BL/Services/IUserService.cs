using BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IUserService
    {
        public Task<IEnumerable<UserInfoDto>> GetUsersWithNameAsync(string name);
    }
}
