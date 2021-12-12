using BL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Facades
{
    public interface IUserFacade
    {
        public Task<List<UserInfoDto>> GetAllUserWithNameAsync(string name);
        public Task<UserInfoDto> GetUserInfo(int userId);
        public Task<List<ReviewDto>> GetUserReviews(int userId);
        public Task<List<ReviewDto>> GetReviewsOnUser(int userId);
        public void UpdateUserInfo(UserUpdateProfileDto user);
        public void ChangePassword(ChangePasswordDto user);
        public Task<List<OfferDto>> GetUserOffers(int userId);
        public Task<List<OfferDto>> GetUserReceivedOffers(int userId);
        public Task<List<TransferDto>> GetUserTransfers(int userId);

        public void CreateUser(UserCreateDto user);
        public Task<UserInfoDto> LoginAsync(UserLoginDto userLogin);
        public Task RegisterUserAsync(UserCreateDto user);
        public Task CheckEmailNotInUse(string email);
    }
}
