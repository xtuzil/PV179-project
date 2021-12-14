using BL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IUserService
    {
        public Task<IEnumerable<UserInfoDto>> GetAll();
        public Task<IEnumerable<UserInfoDto>> GetUsersWithNameAsync(string name);

        public Task<UserInfoDto> GetUserInfo(int userId);

        public Task<UserInfoDto> GetUserWithEmail(string email);

        public void UpdateUserInfo(UserUpdateProfileDto user);
        public void ChangePassword(ChangePasswordDto user);

        public Task RemoveUserMoneyAsync(int userId, double amount);

        public Task AddUserMoneyAsync(int userId, double amount);

        public Task CreateUser(UserCreateDto userDto);

        public Task<UserInfoDto> AuthorizeUserAsync(UserLoginDto login);

        public Task RegisterUser(UserCreateDto user);
        public Task BanUser(int userId, bool ban);
    }
}
