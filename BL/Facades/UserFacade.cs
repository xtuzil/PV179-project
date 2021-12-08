using BL.DTOs;
using BL.Services;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
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
                return (List<UserInfoDto>)await _userService.GetUsersWithNameAsync(name);
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

        public void CreateUser(UserCreateDto user)
        {
            using (var uow = _unitOfWorkProvider.Create())
            {
                _userService.CreateUser(user);
                uow.Commit();

            }
        }

        public void UpdateUserInfo(UserUpdateDto user)
        {
            using (var uow = _unitOfWorkProvider.Create())
            {
                _userService.UpdateUserInfo(user);
                uow.Commit();
            }
        }

        public async Task<List<OfferDto>> GetUserOffers(int userId)
        {
            using (var uow = _unitOfWorkProvider.Create())
            {
                return (List<OfferDto>)await _offerService.GetAuthoredOffersForUser(userId);
            }
        }

        public async Task<List<OfferDto>> GetUserReceivedOffers(int userId)
        {
            using (var uow = _unitOfWorkProvider.Create())
            {
                return (List<OfferDto>)await _offerService.GetReceivedOffersForUser(userId);
            }
        }

        public async Task<List<TransferDto>> GetUserTransfers(int userId)
        {
            using (var uow = _unitOfWorkProvider.Create())
            {
                return (List<TransferDto>)await _transferService.GetTransfersOfUser(userId);
            }
        }

        public async Task<UserInfoDto> LoginAsync(UserLoginDto userLogin)
        {
            var user = await _userService.AuthorizeUserAsync(userLogin);
            if (user != null && !user.Banned)
            {
                return user;
            }
            throw new UnauthorizedAccessException();
        }

        public async Task RegisterUserAsync(UserCreateDto user)
        {
            using (var uow = _unitOfWorkProvider.Create())
            {
                await _userService.RegisterUser(user);
                uow.Commit();
            }
        }

        public async Task CheckEmailNotInUse(string email)
        {
            var user = await _userService.GetUserWithEmail(email);
            if (user != null)
            {
                throw new InvalidOperationException("User with this email already exists.");
            }
        }

        public async Task BanUser(int userId)
        {
            using (var uow = _unitOfWorkProvider.Create())
            {
                await _userService.BanUser(userId, true);
                uow.Commit();
            }
        }

        public async Task UnBanUser(int userId)
        {
            using (var uow = _unitOfWorkProvider.Create())
            {
                await _userService.BanUser(userId, false);
                uow.Commit();
            }
        }


    }
}
