using BL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Facades
{
    public interface IUserFacade
    {
        public Task<IEnumerable<UserInfoDto>> GetAllUsers();
        public Task<IEnumerable<UserInfoDto>> GetAllUserWithNameAsync(string name);
        public Task<UserInfoDto> GetUserInfo(int userId);
        public Task<IEnumerable<ReviewDto>> GetUserReviews(int userId);
        public Task<IEnumerable<ReviewDto>> GetReviewsOnUser(int userId);
        public Task UpdateUserInfo(UserUpdateProfileDto user);
        public Task ChangePassword(ChangePasswordDto user);
        public Task<IEnumerable<OfferDto>> GetUserOffers(int userId);
        public Task<IEnumerable<OfferDto>> GetUserReceivedOffers(int userId);
        public Task<IEnumerable<TransferDto>> GetUserTransfers(int userId);

        public void CreateUser(UserCreateDto user);
        public Task<UserInfoDto> LoginAsync(UserLoginDto userLogin);
        public Task RegisterUserAsync(UserCreateDto user);
        public Task CheckEmailNotInUse(string email);
    }
}
