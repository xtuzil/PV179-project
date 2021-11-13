using BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Facades
{
    public interface IUserFacade
    {
        public Task<List<UserInfoDto>> GetAllUserWithNameAsync(string name);
        public Task<List<UserInfoDto>> GetUserInfo(int userId);
        public Task<List<ReviewDto>> GetUserReviews(int userId);
        public Task<List<ReviewDto>> GetReviewsOnUser(int userId);
        public void UpdateUserInfo(UserUpdateDto user);
        public Task<List<CactusOfferDto>> GetUserOffers(int userId);
        public Task<List<CactusRequestDto>> GetUserRequests(int userId);

    }
}
