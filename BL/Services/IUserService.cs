using BL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IUserService
    {
        public Task<IEnumerable<UserInfoDto>> GetUsersWithNameAsync(string name);

        public Task<UserInfoDto> GetUserWithEmail(string email);

        public Task<UserInfoDto> GetUserInfo(int userId);

        public void UpdateUserInfo(UserUpdateDto user);
        
        public Task<UserInfoDto> AuthorizeUserAsync(UserLoginDto login);

        public Task RegisterUser(UserCreateDto user);
    }
}
