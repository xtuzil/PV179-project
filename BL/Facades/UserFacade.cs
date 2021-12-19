using BL.DTOs;
using BL.Exceptions;
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
        public async Task<IEnumerable<UserInfoDto>> GetAllUsers()
        {
            using (var uow = _unitOfWorkProvider.Create())
            {
                return (List<UserInfoDto>)await _userService.GetAll();
            }
        }
        public async Task<IEnumerable<UserInfoDto>> GetAllUserWithNameAsync(string name)
        {
            using (var uow = _unitOfWorkProvider.Create())
            {
                return (List<UserInfoDto>)await _userService.GetUsersWithNameAsync(name);
            }
        }

        public Task<IEnumerable<ReviewDto>> GetUserReviews(int userId)
        {

            // do we need this?
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ReviewDto>> GetReviewsOnUser(int userId)
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

        public async Task UpdateUserInfo(UserUpdateProfileDto user)
        {
            using (var uow = _unitOfWorkProvider.Create())
            {
                await _userService.UpdateUserInfo(user);
                uow.Commit();
            }
        }

        public async Task ChangePassword(ChangePasswordDto user)
        {
            using (var uow = _unitOfWorkProvider.Create())
            {
                await _userService.ChangePassword(user);
                uow.Commit();
            }
        }

        public async Task<IEnumerable<OfferDto>> GetUserOffers(int userId)
        {
            using (var uow = _unitOfWorkProvider.Create())
            {
                var offers = (List<OfferDto>)await _offerService.GetAuthoredOffersForUser(userId);
                foreach (var offer in offers)
                {
                    var cactusOffer = await _offerService.GetOffer(offer.Id);
                    offer.OfferedCactuses = cactusOffer.OfferedCactuses;
                    offer.RequestedCactuses = cactusOffer.RequestedCactuses;
                }
                return offers;
            }
        }

        public async Task<IEnumerable<OfferDto>> GetUserReceivedOffers(int userId)
        {
            using (var uow = _unitOfWorkProvider.Create())
            {
                var offers = (List<OfferDto>)await _offerService.GetReceivedOffersForUser(userId);
                foreach (var offer in offers)
                {
                    var cactusOffer = await _offerService.GetOffer(offer.Id);
                    offer.OfferedCactuses = cactusOffer.OfferedCactuses;
                    offer.RequestedCactuses = cactusOffer.RequestedCactuses;
                }
                return offers;
            }
        }

        public async Task<IEnumerable<TransferDto>> GetUserTransfers(int userId)
        {
            using (var uow = _unitOfWorkProvider.Create())
            {
                var userOffers = await _offerService.GetTransferedOffersOfUser(userId);

                var transfers = new List<TransferDto> { };

                foreach (var offer in userOffers)
                {
                    transfers.Add(await _transferService.GetTransferByOfferId(offer.Id));
                }

                return transfers;
            }
        }

        public async Task<UserInfoDto> LoginAsync(UserLoginDto userLogin)
        {
            var user = await _userService.AuthorizeUserAsync(userLogin);
            if (user != null)
            {
                if (user.Banned)
                {
                    throw new BannedUserException();
                }

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
    }
}
