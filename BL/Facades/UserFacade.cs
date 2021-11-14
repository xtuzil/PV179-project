using BL.DTOs;
using BL.Services;
using CactusDAL;
using Infrastructure.EntityFramework;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Facades
{
    public class UserFacade : IUserFacade
    {
        private IUserService _userService;
        private IReviewService _reviewService;
        private IOfferService _offerService;
        private ITransferService _transferService;
        private IUnitOfWorkProvider _unitOfWorkProvider;

        public UserFacade(IUnitOfWorkProvider unitOfWorkProvider, IUserService userService,
            IReviewService reviewService, IOfferService offerService, ITransferService transferService)
        {
            _userService = userService;
            _reviewService = reviewService;
            _offerService = offerService;
            _transferService = transferService;
            _unitOfWorkProvider = unitOfWorkProvider;
        }

        public async Task<List<UserInfoDto>> GetAllUserWithNameAsync(string name)
        {
            using (var uow = _unitOfWorkProvider.Create())
            {
                return (List<UserInfoDto>) await _userService.GetUsersWithNameAsync(name);
            }
        }

        public Task<List<ReviewDto>> GetUserReviews(int userId)
        {

            // do we need this?
            throw new NotImplementedException();

        }

        public async Task<List<ReviewDto>> GetReviewsOnUser(int userId)
        {
            using (var uow = _unitOfWorkProvider.Create())
            {
                return (List<ReviewDto>)await _reviewService.GetReviewsOnUser(userId);
            }
        }

        public async Task<UserInfoDto> GetUserInfo(int userId)
        {
            using (var uow = _unitOfWorkProvider.Create())
            {
                return await _userService.GetUserInfo(userId);
            }
        }

        public void UpdateUserInfo(UserUpdateDto user)
        {
            using (var uow = _unitOfWorkProvider.Create())
            {
               _userService.UpdateUserInfo(user);
            }
        }

        public async Task<List<OfferDto>> GetUserOffers(int userId)
        {
            using (var uow = _unitOfWorkProvider.Create())
            {
                return (List<OfferDto>) await _offerService.GetAuthoredOffersForUser(userId);
            }
        }

        public async Task<List<OfferDto>> GetUserReceivedOffers(int userId)
        {
            using (var uow = _unitOfWorkProvider.Create())
            {
                return (List<OfferDto>) await _offerService.GetReceivedOffersForUser(userId);
            }
        }

        public async Task<List<TransferDto>> GetUserTransfers(int userId)
        {
            using (var uow = _unitOfWorkProvider.Create())
            {
                return (List<TransferDto>)await _transferService.GetTransfersOfUser(userId);
            }
        }
    }
}
