using BL.DTOs;
using BL.DTOs.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IUserService
    {
        public Task<IEnumerable<UserInfoDto>> GetUsersWithNameAsync(string name);

        public Task<UserInfoDto> GetUserInfo(int userId);

        public void UpdateUserInfo(UserUpdateDto user);

        public Task RemoveUserMoneyAsync(int userId, double amount);

        public Task AddUserMoneyAsync(int userId, double amount);

        public void CreateUser(UserCreateDto userDto);

    }
}
