using BL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Facades
{
    public interface IUserFacadeEnyem
    {
        public Task<List<UserInfoDto>> GetAllUserWithNameAsync(string name);
        public Task<UserInfoDto> GetUserInfo(int userId);
        public Task<List<ReviewDto>> GetUserReviews(int userId);
        public Task<List<ReviewDto>> GetReviewsOnUser(int userId);
        public void UpdateUserInfo(UserUpdateDto user);
        public Task<List<OfferDto>> GetUserOffers(int userId);
        public Task<List<OfferDto>> GetUserReceivedOffers(int userId);
        public Task<List<TransferDto>> GetUserTransfers(int userId);

        public Task<UserInfoDto> LoginAsync(UserLoginDto userLogin);
        public Task RegisterUserAsync(UserCreateDto user);
        public Task CheckEmailNotInUse(string email);
    }
}
